using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    public static class LessThanRuleFactory
    {
        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max)
            where TValue : IComparable<TValue>
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Greater(max, false));
        }

        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max, IComparer<TValue> comparer)
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Greater(max, false, comparer));
        }
    }
}