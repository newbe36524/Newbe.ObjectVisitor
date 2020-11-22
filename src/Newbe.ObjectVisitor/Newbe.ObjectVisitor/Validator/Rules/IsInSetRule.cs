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
            ICollection<TValue> expectedSet)
        {
            var range = $"{{{string.Join(",", expectedSet)}}}";
            MustExpression = x => expectedSet.Contains(x);
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be in range {range}, but found {value}";
        }

        public Expression<Func<TValue, bool>> MustExpression { get; }
        public Expression<Func<T, TValue, PropertyInfo, string>> ErrorMessageExpression { get; }
    }
}