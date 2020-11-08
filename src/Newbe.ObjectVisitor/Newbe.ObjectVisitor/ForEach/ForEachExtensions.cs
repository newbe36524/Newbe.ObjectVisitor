using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// for each
    /// </summary>
    public static class ForEachExtensions
    {
        #region NoValueExpectedType

        public static IOvBuilderContext<T> ForEach<T>(this IOvBuilderContext<T> builderContext,
            Expression<Action<IObjectVisitorContext<T, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            builderContext.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContext
            });
            return builderContext;
        }

        public static IOvBuilderContext<T> ForEach<T>(this IOvBuilderContext<T> builderContext,
            Expression<Action<string, object>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            builderContext.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValue
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            builderContext.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContextWithExtend
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<string, object, TExtend>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? PropertyInfoFilters.AllPropertyInfo;
            builderContext.Add(new ForEachActionContextItem
            {
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValueWithExtend
            });
            return builderContext;
        }

        #endregion

        #region ValueExpectedType

        public static IOvBuilderContext<T> ForEach<T, TValue>(this IOvBuilderContext<T> builderContext,
            Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            builderContext.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContext
            });
            return builderContext;
        }


        public static IOvBuilderContext<T> ForEach<T, TValue>(this IOvBuilderContext<T> builderContext,
            Expression<Action<string, TValue>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            builderContext.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValue
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend, TValue>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, TValue>>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            builderContext.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContextWithExtend
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend, TValue>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<string, TValue, TExtend>> foreachAction,
            Func<PropertyInfo, bool>? propertyInfoFilter = null)
        {
            var filter = propertyInfoFilter ?? (x => x.PropertyType == typeof(TValue));
            builderContext.Add(new ForEachActionContextItem
            {
                ValueExpectedType = typeof(TValue),
                PropertyInfoFilter = filter,
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValueWithExtend
            });
            return builderContext;
        }

        #endregion
    }
}