using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public static class GreaterThanOrEqualRuleFactory
    {
        public static GreaterThanOrEqualRule<T, TValue> Create<T, TValue>(TValue min)
            where TValue : IComparable<TValue>
        {
            return new GreaterThanOrEqualRule<T, TValue>(min, RuleExpressionHelper.Greater(min, true));
        }

        public static GreaterThanOrEqualRule<T, TValue> Create<T, TValue>(TValue min, IComparer<TValue> comparer)
        {
            return new GreaterThanOrEqualRule<T, TValue>(min, RuleExpressionHelper.Greater(min, true, comparer));
        }
    }
}