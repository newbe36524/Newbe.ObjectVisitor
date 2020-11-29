using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public static class IsInRangeRuleFactory
    {
        public static IsInRangeRule<T, TValue> Create<T, TValue>(
            TValue min,
            TValue max,
            bool excludeMin,
            bool excludeMax)
            where TValue : IComparable<TValue>
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var gtExp = RuleExpressionHelper.Greater(min, excludeMin);
            var ltExp = RuleExpressionHelper.Less(max, excludeMax);
            var bodyExp = gtExp.AndAlso(ltExp);
            var funcExp = Expression.Lambda<Func<TValue, bool>>(bodyExp.Unwrap(pExp), pExp);

            return new IsInRangeRule<T, TValue>(min,
                max,
                excludeMin,
                excludeMax,
                funcExp);
        }

        public static IsInRangeRule<T, TValue> Create<T, TValue>(TValue min,
            TValue max,
            bool excludeMin,
            bool excludeMax,
            IComparer<TValue> comparer)
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var gtExp = RuleExpressionHelper.Greater(min, excludeMin, comparer);
            var ltExp = RuleExpressionHelper.Less(max, excludeMax, comparer);
            var bodyExp = gtExp.AndAlso(ltExp);
            var funcExp = Expression.Lambda<Func<TValue, bool>>(bodyExp.Unwrap(pExp), pExp);
            return new IsInRangeRule<T, TValue>(min,
                max,
                excludeMin,
                excludeMax,
                funcExp);
        }
    }
}