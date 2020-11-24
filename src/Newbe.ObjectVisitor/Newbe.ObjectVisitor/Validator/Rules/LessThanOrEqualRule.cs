using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class LessThanOrEqualRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public LessThanOrEqualRule(TValue max,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be <= {max}, but found {value}";
        }
    }

    public static class LessThanOrEqualRuleFactory
    {
        public static LessThanOrEqualRule<T, TValue> Create<T, TValue>(TValue max)
            where TValue : IComparable<TValue>
        {
            return new LessThanOrEqualRule<T, TValue>(max, RuleExpressionHelper.Greater(max, true));
        }

        public static LessThanOrEqualRule<T, TValue> Create<T, TValue>(TValue max, IComparer<TValue> comparer)
        {
            return new LessThanOrEqualRule<T, TValue>(max, RuleExpressionHelper.Greater(max, true, comparer));
        }
    }
}