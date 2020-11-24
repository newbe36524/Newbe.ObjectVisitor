using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class NotEqualRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public NotEqualRule(TValue expected,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must not be equals to {expected}, but found {value}";
        }
    }

    public static class NotEqualRuleFactory
    {
        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected));
        }

        public static NotEqualRule<T, TValue> Create<T, TValue>(TValue expected, IEqualityComparer<TValue> comparer)
        {
            return new NotEqualRule<T, TValue>(expected, RuleExpressionHelper.NotEqual(expected, comparer));
        }
    }
}