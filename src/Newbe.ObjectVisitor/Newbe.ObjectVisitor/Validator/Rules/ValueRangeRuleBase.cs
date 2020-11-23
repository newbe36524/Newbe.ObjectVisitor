using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public abstract class ValueRangeRuleBase<T, TValue> :PropertyValidationRuleBase<T, TValue> 
        where TValue : IComparable<TValue>
    {
        public static Expression CreateGtExpression(Expression pExp, TValue min, bool excludeMin)
        {
            var minExp = Expression.Constant(min);
            var compareResultExp =
                Expression.Call(pExp, nameof(IComparable<TValue>.CompareTo), Array.Empty<Type>(), minExp);
            var zeroExp = Expression.Constant(0);
            var gtExp = excludeMin
                ? Expression.GreaterThan(compareResultExp, zeroExp)
                : Expression.GreaterThanOrEqual(compareResultExp, zeroExp);
            return gtExp;
        }

        public static Expression CreateLtExpression(Expression pExp, TValue max, bool excludeMax)
        {
            var maxExp = Expression.Constant(max);
            var compareResultExp =
                Expression.Call(pExp, nameof(IComparable<TValue>.CompareTo), Array.Empty<Type>(), maxExp);
            var zeroExp = Expression.Constant(0);
            var ltExp = excludeMax
                ? Expression.LessThan(compareResultExp, zeroExp)
                : Expression.LessThanOrEqual(compareResultExp, zeroExp);
            return ltExp;
        }
    }
}