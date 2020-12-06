using System.Collections;
using System.Linq;

namespace Newbe.ObjectVisitor.Validation
{
    internal class EnumerableNotEmptyRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public EnumerableNotEmptyRule()
        {
            MustExpression = value => value != null && value.Cast<object>().Any();
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must not be empty, but found empty";
        }
    }
}