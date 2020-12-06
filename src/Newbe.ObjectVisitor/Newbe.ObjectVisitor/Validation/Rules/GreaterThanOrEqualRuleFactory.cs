using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class GreaterThanOrEqualRuleFactory
    {
        public static GreaterThanOrEqualRule<T, TValue> Create<T, TValue>(TValue min)
            where TValue : IComparable<TValue>
        {
            return new GreaterThanOrEqualRule<T, TValue>(min, RuleExpressionHelper.Greater(min, false));
        }

        public static GreaterThanOrEqualRule<T, TValue?> CreateNullable<T, TValue>(TValue min)
            where TValue : struct, IComparable<TValue>
        {
            return new GreaterThanOrEqualRule<T, TValue?>(min, RuleExpressionHelper.GreaterNullable(min, false));
        }


        public static GreaterThanOrEqualRule<T, TValue> Create<T, TValue>(TValue min, IComparer<TValue> comparer)
        {
            return new GreaterThanOrEqualRule<T, TValue>(min, RuleExpressionHelper.Greater(min, false, comparer));
        }
    }
}