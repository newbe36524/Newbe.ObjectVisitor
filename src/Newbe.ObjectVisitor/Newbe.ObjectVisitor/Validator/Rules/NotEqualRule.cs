using System;
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
}