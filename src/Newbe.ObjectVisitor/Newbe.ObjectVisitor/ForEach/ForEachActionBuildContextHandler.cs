using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    internal class ForEachActionBuildContextHandler : BuildContextHandlerBase<ForEachActionContextItem>
    {
        public override Expression CreateCore(Type inputType, Type? extendType, ForEachActionContextItem contextItem)
        {
            var inputExp = Expression.Parameter(inputType, "sourceObject");
            if (extendType == null)
            {
                IEnumerable<Expression> blockItems;
                switch (contextItem)
                {
                    case { ExpressionType: ForEachActionContextExpressionType.ObjectVisitorContext }:
                        blockItems = CreateObjectVisitorContextArgs(inputType, inputExp, contextItem);
                        break;
                    case { ExpressionType: ForEachActionContextExpressionType.NameAndValue }:
                        blockItems = CreateNameAndValueArgs(inputType, inputExp, contextItem);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var items = blockItems.ToArray();
                var final = items.Any() ? (Expression) Expression.Block(items) : Expression.Empty();
                var actionType = Expression.GetActionType(inputType);
                var re = Expression.Lambda(actionType, final, inputExp);
                return re;
            }
            else
            {
                var extendP = Expression.Parameter(extendType, "extendObject");
                IEnumerable<Expression> blockItems;
                switch (contextItem)
                {
                    case { ExpressionType: ForEachActionContextExpressionType.ObjectVisitorContextWithExtend }:
                        blockItems = CreateObjectVisitorContextWithExtendArgs(inputType, extendType, inputExp, extendP,
                            contextItem);
                        break;
                    case { ExpressionType: ForEachActionContextExpressionType.NameAndValueWithExtend }:
                        blockItems = CreateNameAndValueWithExtendArgs(inputType, inputExp, extendP, contextItem);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var items = blockItems.ToArray();
                var final = items.Any() ? (Expression) Expression.Block(items) : Expression.Empty();
                var actionType = Expression.GetActionType(inputType, extendType);
                var re = Expression.Lambda(actionType, final, inputExp, extendP);
                return re;
            }
        }

        private IEnumerable<Expression> CreateObjectVisitorContextArgs(Type inputType,
            ParameterExpression inputExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            var propertyInfos = FilterProperties(inputType, forEachActionContextItem);
            var valueType = forEachActionContextItem.ValueExpectedType ?? typeof(object);
            var isValueTypeNotSpecified = forEachActionContextItem.ValueExpectedType == null;
            
            foreach (var propertyInfo in propertyInfos)
            {
                var getterType = Expression.GetFuncType(inputType, valueType);
                var setterType = Expression.GetActionType(inputType, valueType);
                var methodInfo = ObjectVisitorContext.GetCreateMethodInfo(inputType, valueType);
                var getter =
                    isValueTypeNotSpecified
                        ? ValueGetter.Create(inputType, propertyInfo)
                        : ValueGetter.Create(inputType, valueType, propertyInfo);
                var setter =
                    isValueTypeNotSpecified
                        ? ValueSetter.Create(inputType, propertyInfo)
                        : ValueSetter.Create(inputType, valueType, propertyInfo);
                var newExpression = Expression.Call(methodInfo,
                    Expression.Constant(propertyInfo.Name),
                    inputExp,
                    Expression.Constant(propertyInfo),
                    Expression.Constant(getter, getterType),
                    Expression.Constant(setter, setterType));
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction, newExpression);
            }
        }

        private IEnumerable<Expression> CreateObjectVisitorContextWithExtendArgs(Type inputType,
            Type extendType,
            ParameterExpression inputExp,
            ParameterExpression extendExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            var propertyInfos = FilterProperties(inputType, forEachActionContextItem);
            var valueType = forEachActionContextItem.ValueExpectedType ?? typeof(object);
            var isValueTypeNotSpecified = forEachActionContextItem.ValueExpectedType == null;

            foreach (var propertyInfo in propertyInfos)
            {
                var getterType = Expression.GetFuncType(inputType, valueType);
                var setterType = Expression.GetActionType(inputType, valueType);
                var methodInfo = ObjectVisitorContext.GetCreateMethodInfo(inputType, extendType, valueType);
                var getter =
                    isValueTypeNotSpecified
                        ? ValueGetter.Create(inputType, propertyInfo)
                        : ValueGetter.Create(inputType, valueType, propertyInfo);
                var setter =
                    isValueTypeNotSpecified
                        ? ValueSetter.Create(inputType, propertyInfo)
                        : ValueSetter.Create(inputType, valueType, propertyInfo);
                var newExpression = Expression.Call(methodInfo,
                    Expression.Constant(propertyInfo.Name),
                    inputExp,
                    extendExp,
                    Expression.Constant(propertyInfo),
                    Expression.Constant(getter, getterType),
                    Expression.Constant(setter, setterType));
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction, newExpression);
            }
        }

        private IEnumerable<Expression> CreateNameAndValueArgs(Type inputType,
            ParameterExpression inputExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            var propertyInfos = FilterProperties(inputType, forEachActionContextItem);
            var valueType = forEachActionContextItem.ValueExpectedType ?? typeof(object);
            foreach (var propertyInfo in propertyInfos)
            {
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), valueType));
            }
        }

        private IEnumerable<Expression> CreateNameAndValueWithExtendArgs(Type inputType,
            ParameterExpression inputExp,
            ParameterExpression extendExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            var propertyInfos = FilterProperties(inputType, forEachActionContextItem);
            var valueType = forEachActionContextItem.ValueExpectedType ?? typeof(object);
            foreach (var propertyInfo in propertyInfos)
            {
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), valueType),
                    extendExp);
            }
        }

        private static IEnumerable<PropertyInfo> FilterProperties(Type inputType,
            ForEachActionContextItem forEachActionContextItem)
        {
            var runtimeProperties = inputType.GetRuntimeProperties();
            var re = runtimeProperties.Where(forEachActionContextItem.PropertyInfoFilter).ToArray();
            return re;
        }
    }
}