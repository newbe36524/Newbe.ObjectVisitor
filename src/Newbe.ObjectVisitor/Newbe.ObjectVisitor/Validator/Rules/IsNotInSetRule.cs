using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class IsNotInSetRule<T, TValue> : IPropertyValidationRule<T, TValue>
    {
        public IsNotInSetRule(
            ICollection<TValue> expectedSet)
        {
            var range = $"{{{string.Join(",", expectedSet)}}}";
            MustExpression = x => !expectedSet.Contains(x);
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be not in range {range}, but found {value}";
        }

        public Expression<Func<TValue, bool>> MustExpression { get; }
        public Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }

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