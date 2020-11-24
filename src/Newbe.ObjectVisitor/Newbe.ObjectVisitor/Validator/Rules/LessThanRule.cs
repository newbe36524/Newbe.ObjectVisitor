using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class LessThanRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public LessThanRule(TValue max,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be < {max}, but found {value}";
        }
    }

    public static class LessThanRuleFactory
    {
        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max)
            where TValue : IComparable<TValue>
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Greater(max, false));
        }

        public static LessThanRule<T, TValue> Create<T, TValue>(TValue max, IComparer<TValue> comparer)
        {
            return new LessThanRule<T, TValue>(max, RuleExpressionHelper.Greater(max, false, comparer));
        }
    }
}