using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validation
{
    internal static class LessThanRuleFactory
    {
        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max)
            where TValue : IComparable<TValue>
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Less(max, true));
        }

        public static LessThanRule<T, TValue?> CreateNullable<T, TValue>(TValue max)
            where TValue : struct, IComparable<TValue>
        {
            return new LessThanRule<T, TValue?>(max, RuleExpressionHelper.LessNullable(max, true));
        }

        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max, IComparer<TValue> comparer)
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Less(max, true, comparer));
        }
    }
}