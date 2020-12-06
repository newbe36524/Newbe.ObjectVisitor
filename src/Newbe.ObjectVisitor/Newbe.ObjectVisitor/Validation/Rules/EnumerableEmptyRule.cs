using System.Collections;
using System.Linq;

namespace Newbe.ObjectVisitor.Validation
{
    internal class EnumerableEmptyRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public EnumerableEmptyRule()
        {
            MustExpression = value => value == null || !value.Cast<object>().Any();
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must be empty, but found some value";
        }
    }
}