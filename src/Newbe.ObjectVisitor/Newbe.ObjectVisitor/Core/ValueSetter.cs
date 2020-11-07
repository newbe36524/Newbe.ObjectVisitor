using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public static class ValueSetter
    {
        public static object Get(Type objType, Type valueType, PropertyInfo info)
        {
            var bodyExp = Expression.Call(
                typeof(ValueSetter<,,>).MakeGenericType(objType, info.PropertyType, valueType),
                nameof(ValueSetter<object, object, object>.GetSetter),
                Array.Empty<Type>(),
                Expression.Constant(info));
            var finalExp = Expression.Lambda<Func<object>>(bodyExp);
            var func = finalExp.Compile();
            var re = func.Invoke();
            return re;
        }

        public static object Get(Type objType, PropertyInfo info)
        {
            var bodyExp = Expression.Call(typeof(ValueSetter<>).MakeGenericType(objType),
                nameof(ValueSetter<object, object, object>.GetSetter),
                Array.Empty<Type>(),
                Expression.Constant(info));
            var finalExp = Expression.Lambda<Func<object>>(bodyExp);
            var func = finalExp.Compile();
            var re = func.Invoke();
            return re;
        }

        internal static SwitchCase CreateSetterCase<TTargetObject, TTargetValue>(PropertyInfo propertyInfo)
        {
            var sourceObjExp = Expression.Parameter(typeof(TTargetObject), "sourceObj");
            var valueExp = Expression.Parameter(typeof(TTargetValue), "value");
            var newValueExp = Expression.Convert(valueExp, propertyInfo.PropertyType);
            var bodyExp = Expression.Assign(Expression.Property(sourceObjExp, propertyInfo), newValueExp);
            var finalExp =
                Expression.Lambda<Action<TTargetObject, TTargetValue>>(bodyExp, sourceObjExp, valueExp);
            var getter = finalExp.Compile();
            var caseExp = Expression.Constant(propertyInfo);
            return Expression.SwitchCase(Expression.Constant(getter), caseExp);
        }

        internal static Func<PropertyInfo, Action<TTargetObject, TTargetValue>> CreateSetter<TTargetObject,
            TTargetValue>(
            IEnumerable<PropertyInfo> propertyInfos,
            Func<PropertyInfo, SwitchCase> caseFactory)
        {
            var pExp = Expression.Parameter(typeof(PropertyInfo), "info");
            var cases = propertyInfos.Select(caseFactory);
            var switchExp =
                Expression.Switch(pExp,
                    Expression.Constant(null,
                        typeof(Action<TTargetObject, TTargetValue>)),
                    null,
                    cases);
            var funcExp = Expression.Lambda<Func<PropertyInfo, Action<TTargetObject, TTargetValue>>>(switchExp, pExp);
            var re = funcExp.Compile();
            return re;
        }
    }

    public static class ValueSetter<TTargetObject, TPropertyValue, TTargetValue>
    {
        private static readonly Func<PropertyInfo, Action<TTargetObject, TTargetValue>> Finder;

        static ValueSetter()
        {
            var propertyInfos = typeof(TTargetObject).GetRuntimeProperties()
                .Where(x => x.CanWrite)
                .Where(x => x.PropertyType == typeof(TPropertyValue));

            Finder = ValueSetter.CreateSetter<TTargetObject, TTargetValue>(propertyInfos,
                ValueSetter.CreateSetterCase<TTargetObject, TTargetValue>);
        }

        public static Action<TTargetObject, TTargetValue> GetSetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }

    public static class ValueSetter<TTargetObject>
    {
        private static readonly Func<PropertyInfo, Action<TTargetObject, object>> Finder;

        static ValueSetter()
        {
            var propertyInfos = typeof(TTargetObject).GetRuntimeProperties()
                .Where(x => x.CanWrite);

            Finder = ValueSetter.CreateSetter<TTargetObject, object>(propertyInfos,
                ValueSetter.CreateSetterCase<TTargetObject, object>);
        }

        public static Action<TTargetObject, object> GetSetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }
}