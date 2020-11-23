using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class IsInRangeRule<T, TValue> : ValueRangeRuleBase<T, TValue>
        where TValue : IComparable<TValue>
    {
        public IsInRangeRule(
            TValue min,
            TValue max,
            bool excludeMin = false,
            bool excludeMax = true)
        {
            MustExpression = CreateIsInRangeExp(min, max, excludeMin, excludeMax);
            ErrorMessageExpression = CreateIsInRangeErrorExp(min, max, excludeMin, excludeMax);
        }

        private static Expression<Func<TValue, bool>> CreateIsInRangeExp(TValue min, TValue max,
            bool excludeMin,
            bool excludeMax)
        {
            var pExp = Expression.Parameter(typeof(TValue), "value");
            var gtExp = CreateGtExpression(pExp, min, excludeMin);
            var ltExp = CreateLtExpression(pExp, max, excludeMax);
            var bodyExp = Expression.AndAlso(gtExp, ltExp);
            var funcExp = Expression.Lambda<Func<TValue, bool>>(bodyExp, pExp);
            return funcExp;
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