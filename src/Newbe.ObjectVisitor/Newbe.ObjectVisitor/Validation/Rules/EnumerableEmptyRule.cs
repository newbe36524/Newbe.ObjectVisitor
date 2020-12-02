using System.Collections;

namespace Newbe.ObjectVisitor.Validation
{
    public class EnumerableEmptyRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : IEnumerable
    {
        public EnumerableEmptyRule()
        {
            MustExpression = value => !value.GetEnumerator().MoveNext();
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must be empty, but found some value";
        }
    }
}