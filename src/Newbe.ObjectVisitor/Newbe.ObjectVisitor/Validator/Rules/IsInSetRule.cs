using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class IsInSetRule<T, TValue> : IPropertyValidationRule<T, TValue>
        where TValue : IEquatable<TValue>
    {
        public IsInSetRule(
            IEnumerable<TValue> expectedSet)
        {
            var set = new HashSet<TValue>();
            foreach (var value in expectedSet)
            {
                set.Add(value);
            }

            var range = $"{{{string.Join(",", set)}}}";
            MustExpression = x => set.Contains(x);
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be in range {range}, but found {value}";
        }

        public Expression<Func<TValue, bool>> MustExpression { get; }
        public Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }
}