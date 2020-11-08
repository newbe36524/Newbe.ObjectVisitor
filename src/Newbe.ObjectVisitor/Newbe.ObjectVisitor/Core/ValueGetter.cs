using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public static class ValueGetter
    {
        public static object Get(Type objType, Type valueType, PropertyInfo info)
        {
            var bodyExp = Expression.Call(
                typeof(ValueGetter<,,>).MakeGenericType(objType, info.PropertyType, valueType),
                nameof(ValueGetter<object, object, object>.GetGetter),
                Array.Empty<Type>(),
                Expression.Constant(info));
            var finalExp = Expression.Lambda<Func<object>>(bodyExp);
            var func = finalExp.Compile();
            var re = func.Invoke();
            return re;
        }

        public static object Get(Type objType, PropertyInfo info)
        {
            var bodyExp = Expression.Call(typeof(ValueGetter<>).MakeGenericType(objType),
                nameof(ValueGetter<object>.GetGetter),
                Array.Empty<Type>(),
                Expression.Constant(info));
            var finalExp = Expression.Lambda<Func<object>>(bodyExp);
            var func = finalExp.Compile();
            var re = func.Invoke();
            return re;
        }

        internal static SwitchCase CreateGetterCase<TTargetObject, TTargetValue>(PropertyInfo propertyInfo)
        {
            var sourceObjExp = Expression.Parameter(typeof(TTargetObject), "sourceObj");
            var finalExp =
                Expression.Lambda<Func<TTargetObject, TTargetValue>>(
                    Expression.Convert(Expression.Property(sourceObjExp, propertyInfo), typeof(TTargetValue)),
                    sourceObjExp);
            var getter = finalExp.Compile();
            var caseExp = Expression.Constant(propertyInfo);
            return Expression.SwitchCase(Expression.Constant(getter), caseExp);
        }

        internal static Func<PropertyInfo, Func<TTargetObject, TTargetValue>> CreateGetter<TTargetObject, TTargetValue>(
            IEnumerable<PropertyInfo> propertyInfos,
            Func<PropertyInfo, SwitchCase> caseFactory)
        {
            var pExp = Expression.Parameter(typeof(PropertyInfo), "info");
            var cases = propertyInfos.Select(caseFactory);

            var switchExp =
                Expression.Switch(pExp,
                    Expression.Constant(null,
                        typeof(Func<TTargetObject, TTargetValue>)),
                    null,
                    cases);
            var funcExp = Expression.Lambda<Func<PropertyInfo, Func<TTargetObject, TTargetValue>>>(switchExp, pExp);
            var re = funcExp.Compile();
            return re;
        }
    }

    public static class ValueGetter<TTargetObject, TPropertyValue, TTargetValue>
    {
        private static readonly Func<PropertyInfo, Func<TTargetObject, TTargetValue>> Finder;

        static ValueGetter()
        {
            var propertyInfos = typeof(TTargetObject).GetRuntimeProperties()
                .Where(x => x.CanRead)
                .Where(x => x.PropertyType == typeof(TPropertyValue));

            Finder = ValueGetter.CreateGetter<TTargetObject, TTargetValue>(propertyInfos,
                ValueGetter.CreateGetterCase<TTargetObject, TTargetValue>);
        }

        public static Func<TTargetObject, TTargetValue> GetGetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }

    public static class ValueGetter<TTargetObject>
    {
        private static readonly Func<PropertyInfo, Func<TTargetObject, object>> Finder;

        static ValueGetter()
        {
            var propertyInfos = typeof(TTargetObject).GetRuntimeProperties()
                .Where(x => x.CanRead);

            Finder = ValueGetter.CreateGetter<TTargetObject, object>(propertyInfos,
                ValueGetter.CreateGetterCase<TTargetObject, object>);
        }

        public static Func<TTargetObject, object> GetGetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }
}