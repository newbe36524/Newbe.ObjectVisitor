using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Helper class to generate a func to get value from a object property
    /// </summary>
    public static class ValueGetter
    {
        /// <summary>
        /// Create a getter func to get value from a object property.
        /// <example>
        /// var stringLengthFunc = (Func&lt;string,int&gt;) ValueGetter.Create(typeof(string), typeof(int), typeof(string).GetProperty("Length"))
        /// </example>
        /// </summary>
        /// <param name="objType">Type of target object</param>
        /// <param name="valueType">Type of target property</param>
        /// <param name="info">PropertyInfo of target property</param>
        /// <returns>A Func as Func&lt;objType,valueType&gt;. </returns>
        public static object Create(Type objType, Type valueType, PropertyInfo info)
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

        /// <summary>
        /// Create a getter func to get value from a object property.
        /// <example>
        /// var stringLengthFunc = (Func&lt;string,object&gt;) ValueGetter.Create(typeof(string), typeof(string).GetProperty("Length"))
        /// </example>
        /// </summary>
        /// <param name="objType">Type of target object</param>
        /// <param name="info">PropertyInfo of target property</param>
        /// <returns>A Func as Func&lt;objType,object&gt;. </returns>
        public static object Create(Type objType, PropertyInfo info)
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

    /// <summary>
    /// Value getter in generic format.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of target object</typeparam>
    /// <typeparam name="TPropertyValue">Type of property</typeparam>
    /// <typeparam name="TTargetValue">Type of target value. This is used as return value type of func, it can be different from <typeparamref name="TPropertyValue"/>. You must confirm that <typeparamref name="TPropertyValue"/> can be directly cast to <typeparam name="TTargetValue"/>, It will throw a exception otherwise.</typeparam>
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

        /// <summary>
        /// Create a getter func to get property value from a object property.
        /// <example>
        /// Func&lt;string,int&gt; stringLengthFunc =  ValueGetter&lt;string, int, int&gt;.GetGetter(typeof(string).GetProperty("Length"))
        /// </example>
        /// </summary>
        /// <param name="info">PropertyInfo of target property</param>
        /// <returns>Func as a value getter</returns>
        public static Func<TTargetObject, TTargetValue> GetGetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }

    /// <summary>
    /// Value getter in no-generic format.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of target object</typeparam>
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

        /// <summary>
        /// Create a getter func to get property value from a object property.
        /// <example>
        /// Func&lt;string,object&gt; stringLengthFunc =  ValueGetter&lt;string&gt;.GetGetter(typeof(string).GetProperty("Length"))
        /// </example>
        /// </summary>
        /// <param name="info">PropertyInfo of target property</param>
        /// <returns>Func as a value getter</returns>
        public static Func<TTargetObject, object> GetGetter(PropertyInfo info)
        {
            return Finder.Invoke(info);
        }
    }
}