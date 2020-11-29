using System;
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
}