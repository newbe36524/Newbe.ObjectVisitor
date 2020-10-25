using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// for each
    /// </summary>
    public static class ForEachExtensions
    {
        public static IOvBuilderContext<T> ForEach<T>(this IOvBuilderContext<T> builderContext,
            Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
        {
            builderContext.Add(new ForEachActionContextItem
            {
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContext
            });
            return builderContext;
        }

        public static IOvBuilderContext<T> ForEach<T>(this IOvBuilderContext<T> builderContext,
            Expression<Action<string, object>> foreachAction)
        {
            builderContext.Add(new ForEachActionContextItem
            {
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValue
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<IObjectVisitorContext<T, TExtend, object>>> foreachAction)
        {
            builderContext.Add(new ForEachActionContextItem
            {
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.ObjectVisitorContextWithExtend
            });
            return builderContext;
        }

        public static IOvBuilderContext<T, TExtend> ForEach<T, TExtend>(
            this IOvBuilderContext<T, TExtend> builderContext,
            Expression<Action<string, object, TExtend>> foreachAction)
        {
            builderContext.Add(new ForEachActionContextItem
            {
                ForEachAction = foreachAction,
                ExpressionType = ForEachActionContextExpressionType.NameAndValueWithExtend
            });
            return builderContext;
        }
    }
}