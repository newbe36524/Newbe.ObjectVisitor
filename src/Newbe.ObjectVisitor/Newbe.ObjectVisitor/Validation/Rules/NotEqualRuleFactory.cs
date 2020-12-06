using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class NotEqualRuleFactory
    {
        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected));
        }

        public static NotEqualRule<T, TValue?> Create<T, TValue>(TValue? expected)
            where TValue : struct, IEquatable<TValue>
        {
            return new NotEqualRule<T, TValue?>(expected, RuleExpressionHelper.NotEqual(expected));
        }

        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected, IEqualityComparer<TValue> comparer)
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected, comparer));
        }

        public static NotEqualRule<T, TValue?> Create<T, TValue>(TValue? expected, IEqualityComparer<TValue?> comparer)
            where TValue : struct
        {
            return new NotEqualRule<T, TValue?>(expected, RuleExpressionHelper.NotEqual(expected, comparer));
        }
    }
}