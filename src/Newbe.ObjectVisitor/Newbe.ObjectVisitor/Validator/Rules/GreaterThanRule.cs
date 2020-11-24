using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class GreaterThanRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public GreaterThanRule(TValue min,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be > {min}, but found {value}";
        }
    }

    public static class GreaterThanRuleFactory
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