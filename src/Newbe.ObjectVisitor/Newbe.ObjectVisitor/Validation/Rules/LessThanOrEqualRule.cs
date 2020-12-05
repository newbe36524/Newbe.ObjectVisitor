using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class LessThanOrEqualRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public LessThanOrEqualRule(TValue max,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be <= {max}, but found {value}";
        }
    }
}