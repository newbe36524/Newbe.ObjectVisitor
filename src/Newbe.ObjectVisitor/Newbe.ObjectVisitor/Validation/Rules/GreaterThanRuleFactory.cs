using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class GreaterThanRuleFactory
    {
        public static GreaterThanRule<T, TValue> Create<T, TValue>(TValue min)
            where TValue : IComparable<TValue>
        {
            return new GreaterThanRule<T, TValue>(min, RuleExpressionHelper.Greater(min, true));
        }

        public static GreaterThanRule<T, TValue> Create<T, TValue>(TValue min, IComparer<TValue> comparer)
        {
            return new GreaterThanRule<T, TValue>(min, RuleExpressionHelper.Greater(min, true, comparer));
        }
    }
}