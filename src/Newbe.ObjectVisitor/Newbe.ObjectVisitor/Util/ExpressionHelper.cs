using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> exp)
        {
            var visitor = new PropertyExpressionVisitor();
            var propertyInfo = visitor.GetExpression(exp);
            return propertyInfo;
        }

        private class PropertyExpressionVisitor : ExpressionVisitor
        {
            private MemberExpression? _firstMemberAccessExpression = null;

            public PropertyInfo? GetExpression(Expression source)
            {
                this.Visit(source);
                return _firstMemberAccessExpression!.Member as PropertyInfo;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                _firstMemberAccessExpression ??= node;
                return base.VisitMember(node);
            }
        }
        
        public static Expression<Func<T, TValue, PropertyInfo, bool>> CreateValidateExpression<T, TValue>(
            Expression<Func<TValue, bool>> func)
        {
            var inputExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var pExp = Expression.Parameter(typeof(PropertyInfo), "p");
            var bodyExp = Expression.Invoke(func, valueExp);
            var funcExp = Expression.Lambda<Func<T, TValue, PropertyInfo, bool>>(bodyExp, inputExp, valueExp, pExp);
            return funcExp;
        }
    }
}