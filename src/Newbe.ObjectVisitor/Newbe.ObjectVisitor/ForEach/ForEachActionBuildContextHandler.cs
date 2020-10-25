using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public class ForEachActionBuildContextHandler : BuildContextHandlerBase<ForEachActionContextItem>
    {
        public override Expression CreateCore(Type inputType, Type? extendType, ForEachActionContextItem contextItem)
        {
            var inputExp = Expression.Parameter(inputType, "sourceObject");
            if (extendType == null)
            {
                var actionType = Expression.GetActionType(inputType);
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

                var final = Expression.Block(blockItems);
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

                var final = Expression.Block(blockItems);
                var actionType = Expression.GetActionType(inputType, extendType);
                var re = Expression.Lambda(actionType, final, inputExp, extendP);
                return re;
            }
        }

        private IEnumerable<Expression> CreateObjectVisitorContextArgs(Type inputType,
            ParameterExpression inputExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            foreach (var propertyInfo in inputType.GetRuntimeProperties())
            {
                var methodInfo = ObjectVisitorContext.GetCreateMethodInfo(inputType, typeof(object));
                var newExpression = Expression.Call(methodInfo,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), typeof(object)),
                    inputExp,
                    Expression.Constant(propertyInfo));
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction, newExpression);
            }
        }

        private IEnumerable<Expression> CreateObjectVisitorContextWithExtendArgs(Type inputType,
            Type extendType,
            ParameterExpression inputExp,
            ParameterExpression extendExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            foreach (var propertyInfo in inputType.GetRuntimeProperties())
            {
                var methodInfo = ObjectVisitorContext.GetCreateMethodInfo(inputType, extendType, typeof(object));
                var newExpression = Expression.Call(methodInfo,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), typeof(object)),
                    inputExp,
                    extendExp,
                    Expression.Constant(propertyInfo));
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction, newExpression);
            }
        }

        private IEnumerable<Expression> CreateNameAndValueArgs(Type inputType,
            ParameterExpression inputExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            foreach (var propertyInfo in inputType.GetRuntimeProperties())
            {
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), typeof(object)));
            }
        }

        private IEnumerable<Expression> CreateNameAndValueWithExtendArgs(Type inputType,
            ParameterExpression inputExp,
            ParameterExpression extendExp,
            ForEachActionContextItem forEachActionContextItem)
        {
            foreach (var propertyInfo in inputType.GetRuntimeProperties())
            {
                yield return Expression.Invoke(forEachActionContextItem.ForEachAction,
                    Expression.Constant(propertyInfo.Name),
                    Expression.Convert(Expression.Property(inputExp, propertyInfo), typeof(object)),
                    extendExp);
            }
        }
    }
}