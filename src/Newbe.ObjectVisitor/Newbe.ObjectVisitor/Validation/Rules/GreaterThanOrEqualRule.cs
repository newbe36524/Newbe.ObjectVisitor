using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class GreaterThanOrEqualRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public GreaterThanOrEqualRule(TValue min,
            Expression<Func<TValue, bool>> must)
        {
            MustExpression = must;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must be >= {min}, but found {value}";
        }
    }
}