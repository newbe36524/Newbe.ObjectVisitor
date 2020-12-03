using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Extension about <see cref="OVBuilder{T}.IOVBuilder_V.ForEach"/>
    /// </summary>
    public static class ForEachExtensions
    {
        #region NoValueExpectedType

        public static OVBuilder<T>.IOVBuilder_V ForEach<T>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<IObjectVisitorContext<T, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilder<T>.IOVBuilder_V ForEach<T>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<string, object>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<string, object, TExtend>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        #endregion

        #region ValueExpectedType

        public static OVBuilder<T>.IOVBuilder_V ForEach<T, TValue>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilder<T>.IOVBuilder_V ForEach<T, TValue>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<string, TValue>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend, TValue>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend, TValue>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<string, TValue, TExtend>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        #endregion
    }
}