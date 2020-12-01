using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public static class NotEqualRuleFactory
    {
        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected));
        }

        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected, IEqualityComparer<TValue> comparer)
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected, comparer));
        }
    }
}