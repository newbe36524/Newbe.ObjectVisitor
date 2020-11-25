using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AgileObjects.NetStandardPolyfills;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class IsInEnumRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public IsInEnumRule(
            Type enumType,
            bool? flagged = null)
        {
            bool hasFlag;
            hasFlag = flagged ?? enumType.HasAttribute<FlagsAttribute>();

            var values = Enum.GetValues(enumType)
                .Cast<Enum>()
                .Select(x => x.ToString("D"))
                .Select(long.Parse)
                .ToArray();
            if (hasFlag)
            {
                values = GetFlagOrValue(values).ToArray();
            }

            var set = new HashSet<long>();
            foreach (var value in values)
            {
                set.Add(value);
            }

            if (typeof(TValue) == typeof(string))
            {
                var names = Enum.GetNames(enumType).ToArray();
                MustExpression = value =>
                    names.Contains((string) (object) value!, CaseInsensitiveStringComparer.Instance);
            }
            else
            {
                var valueExp = Expression.Parameter(typeof(TValue), "value");
                var bodyExp = Expression.Call(
                    Expression.Constant(set),
                    nameof(HashSet<int>.Contains),
                    Array.Empty<Type>(),
                    Expression.Convert(valueExp, typeof(long)));
                var mustExp = Expression.Lambda<Func<TValue, bool>>(bodyExp, valueExp);
                MustExpression = mustExp;
            }

            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be in value defined in enum {enumType.Name}, but found {value}";
        }

        private class CaseInsensitiveStringComparer : IEqualityComparer<string>
        {
            public static CaseInsensitiveStringComparer Instance { get; } = new CaseInsensitiveStringComparer();

            public bool Equals(string? x, string? y)
            {
                return x?.ToUpper() == y?.ToUpper();
            }

            public int GetHashCode(string? obj)
            {
                return obj?.ToUpper().GetHashCode() ?? 0;
            }
        }

        private static IEnumerable<long> GetFlagOrValue(IReadOnlyList<long> values)
        {
            if (values.Count == 1)
            {
                yield return values[0];
            }
            else
            {
                var first = values.First();
                yield return first;
                var left = values.Skip(1).ToArray();
                var flagOrValue = GetFlagOrValue(left).ToArray();
                foreach (var value in flagOrValue)
                {
                    yield return value;
                    yield return first | value;
                }
            }
        }
    }

    public static class IsInEnumRuleFactory
    {
        public static IsInEnumRule<T, TValue> Create<T, TValue>(
            Type enumType,
            bool? flagged = null)
        {
            return new IsInEnumRule<T, TValue>(enumType,
                flagged);
        }
    }
}