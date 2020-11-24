using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class EqualRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public EqualRule(TValue expected,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be equals to {expected}, but found {value}";
        }
    }

    public static class EqualRuleFactory
    {
        public static EqualRule<T, TValue> Create<T, TValue>(TValue expected)
            where TValue : IEquatable<TValue>
        {
            return new EqualRule<T, TValue>(expected, RuleExpressionHelper.Equal(expected));
        }

        public static EqualRule<T, TValue> Create<T, TValue>(TValue expected, IEqualityComparer<TValue> comparer)
        {
            return new EqualRule<T, TValue>(expected, RuleExpressionHelper.Equal(expected, comparer));
        }
    }
}