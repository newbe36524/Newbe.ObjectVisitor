using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    public class IsInRangeRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public IsInRangeRule(
            TValue min,
            TValue max,
            bool excludeMin,
            bool excludeMax,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = CreateIsInRangeErrorExp(min, max, excludeMin, excludeMax);
        }

        private static Expression<Func<T, TValue, PropertyInfo, string>> CreateIsInRangeErrorExp(
            TValue min,
            TValue max,
            bool excludeMin,
            bool excludeMax)
        {
            var range = $"{(excludeMin ? '(' : '[')}{min}, {max}{(excludeMax ? ')' : ']')}";
            return CreateCore();

            Expression<Func<T, TValue, PropertyInfo, string>> CreateCore()
            {
                return (input, value, p) =>
                    $"Value of {p.Name} must be in range {range}, but found {value}";
            }
        }
    }
}