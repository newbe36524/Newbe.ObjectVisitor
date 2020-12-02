using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
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
}