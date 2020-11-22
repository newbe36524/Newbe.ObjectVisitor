using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public abstract class ValueRangeRuleBase<T, TValue> : IPropertyValidationRule<T, TValue>
        where TValue : IComparable<TValue>
    {
        public abstract Expression<Func<TValue, bool>> MustExpression { get; }
        public abstract Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }

        public static Expression CreateGtExpression(Expression pExp, TValue min, bool excludeMin)
        {
            var minExp = Expression.Constant(min);
            var gtExp = excludeMin
                ? Expression.GreaterThan(pExp, minExp)
                : Expression.GreaterThanOrEqual(pExp, minExp);
            return gtExp;
        }

        public static Expression CreateLtExpression(Expression pExp, TValue max, bool excludeMax)
        {
            var maxExp = Expression.Constant(max);
            var ltExp = excludeMax
                ? Expression.LessThan(pExp, maxExp)
                : Expression.LessThanOrEqual(pExp, maxExp);
            return ltExp;
        }
    }
}