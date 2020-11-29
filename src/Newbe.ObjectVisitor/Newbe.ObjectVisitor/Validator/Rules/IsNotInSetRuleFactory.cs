using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public static class IsNotInSetRuleFactory
    {
        public static IsNotInSetRule<T, TValue> Create<T, TValue>(IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var set = new HashSet<TValue>();
            foreach (var item in expectedSet)
            {
                set.Add(item);
            }

            return new IsNotInSetRule<T, TValue>(set);
        }

        public static IsNotInSetRule<T, TValue> Create<T, TValue>(IEnumerable<TValue> expectedSet,
            IEqualityComparer<TValue> comparer)
        {
            var set = new HashSet<TValue>(comparer);
            foreach (var item in expectedSet)
            {
                set.Add(item);
            }

            return new IsNotInSetRule<T, TValue>(set);
        }
    }
}