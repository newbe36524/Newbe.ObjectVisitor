using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Helper about <see cref="Expression"/>
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Get <see cref="PropertyInfo"/> of a expression body
        /// </summary>
        /// <param name="exp">Expression to be checked</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> exp)
        {
            var visitor = new PropertyExpressionVisitor();
            var propertyInfo = visitor.GetExpression(exp);
            return propertyInfo!;
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

        /// <summary>
        /// Join <paramref name="exp1"/> and <paramref name="exp2"/> into a new expression with &amp;&amp;
        /// </summary>
        /// <example>
        /// <code>
        /// Expression&lt;int,bool&gt; exp1 = x =&gt; x &gt; 2; 
        /// Expression&lt;int,bool&gt; exp2 = x =&gt; x &lt; 10;
        /// var exp3 = exp1.AndAlso(exp1);
        /// // exp3 should be as x =&gt; x &gt; 2 &amp;&amp; x &lt; 10
        /// </code>
        /// </example>
        /// <param name="exp1">First expression</param>
        /// <param name="exp2">Second expression</param>
        /// <typeparam name="T">Type a expression func input</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp1,
            Expression<Func<T, bool>> exp2)
        {
            var pExp = Expression.Parameter(typeof(T), "x");
            var exp1Body = exp1.Unwrap(pExp);
            var exp2Body = exp2.Unwrap(pExp);
            var bodyExp = Expression.AndAlso(exp1Body, exp2Body);
            return Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
        }

        /// <summary>
        /// Unwrap a <see cref="LambdaExpression"/> into a <see cref="Expression"/> by replacing parameters
        /// </summary>
        /// <example>
        /// <code>
        /// Expression&lt;int,bool&gt; exp1 = x =&gt; x &gt; 2;
        /// var bodyExp1 = exp1.Unwrap(Expression.Constant(1));
        /// // bodyExp1 should be a new Expression like 1 &gt; 2
        /// // please notice, the new expression is not a lambda.
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// Expression&lt;int,bool&gt; exp1 = x =&gt; x &gt; 2;
        /// var stringExp = Expression.Parameter(typeof(string), "str");
        /// var stringLengthExp = Expression.Property(stringExp, nameof(string.Length));
        /// var bodyExp1 = exp1.Unwrap(stringLengthExp);
        /// var stringLengthCompareExp = Expression.Lambda&lt;Func&lt;string, bool&gt;&gt;(bodyExp1, stringExp);
        /// // stringLengthCompareExp should be a new expression like str=&gt;str.Length > 2
        /// </code>
        /// </example>
        /// <param name="lambdaExpression">lambda expression to be unwrapped</param>
        /// <param name="parameterExpression">replacement expressions to parameters of <paramref name="lambdaExpression"/></param>
        /// <returns></returns>
        public static Expression Unwrap(this LambdaExpression lambdaExpression, params Expression[] parameterExpression)
        {
            var ps = lambdaExpression.Parameters;
            var dic = new Dictionary<ParameterExpression, Expression>();
            for (var i = 0; i < ps.Count; i++)
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