using System.Collections;

namespace Newbe.ObjectVisitor.Validation
{
    internal class EnumerableNotEmptyRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public EnumerableNotEmptyRule()
        {
            MustExpression = value => value.GetEnumerator().MoveNext();
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must not be empty, but found empty";
        }
    }
}