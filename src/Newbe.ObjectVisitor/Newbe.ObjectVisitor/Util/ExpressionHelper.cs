using System;
using System.Collections.Generic;
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

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp1,
            Expression<Func<T, bool>> exp2)
        {
            var pExp = Expression.Parameter(typeof(T), "x");
            var exp1Body = exp1.Unwrap(pExp);
            var exp2Body = exp2.Unwrap(pExp);
            var bodyExp = Expression.AndAlso(exp1Body, exp2Body);
            return Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
        }
        
        public static Expression Unwrap(this LambdaExpression lambdaExpression, params Expression[] parameterExpression)
        {
            var ps = lambdaExpression.Parameters;
            var dic = new Dictionary<ParameterExpression, Expression>();
            for (int i = 0; i < ps.Count; i++)
            {
                var p = ps[i];
                dic.Add(p, parameterExpression[i]);
            }

            var visitor = new UnwrapVisitor(dic);
            var re = visitor.Visit(lambdaExpression.Body);
            return re;
        }

        private class UnwrapVisitor : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, Expression> _replacementDic;

            public UnwrapVisitor(
                Dictionary<ParameterExpression, Expression> replacementDic)
            {
                _replacementDic = replacementDic;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (_replacementDic.TryGetValue(node, out var value))
                {
                    return value;
                }

                return base.VisitParameter(node);
            }
        }
    }
}