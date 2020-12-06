using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Extension about ForEach
    /// </summary>
    public static class ForEachExtensions
    {
        #region NoValueExpectedType

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilder<T>.IOVBuilder_V ForEach<T>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<IObjectVisitorContext<T, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilder<T>.IOVBuilder_V ForEach<T>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<string, object>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
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

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TValue">Type of property</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilder<T>.IOVBuilder_V ForEach<T, TValue>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TValue">Type of property</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilder<T>.IOVBuilder_V ForEach<T, TValue>(
            this OVBuilder<T>.IOVBuilder_V builderContext,
            Expression<Action<string, TValue>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TValue">Type of property</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
        public static OVBuilderExt<T, TExtend>.IOVBuilderExt_V ForEach<T, TExtend, TValue>(
            this OVBuilderExt<T, TExtend>.IOVBuilderExt_V builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            return builderContext
                .FilterProperty(propertyInfoFilter)
                .ForEach(foreachAction);
        }

        /// <summary>
        /// Register object visiting operation
        /// </summary>
        /// <param name="builderContext">Context of object visitor builder</param>
        /// <param name="foreachAction">Action of object visiting</param>
        /// <param name="propertyInfoFilter">Filter properties of target object those should be visited</param>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <typeparam name="TValue">Type of property</typeparam>
        /// <typeparam name="TExtend">Type of extend data</typeparam>
        /// <returns>Object visitor builder</returns>
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