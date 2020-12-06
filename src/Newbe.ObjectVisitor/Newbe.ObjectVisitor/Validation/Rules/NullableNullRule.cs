namespace Newbe.ObjectVisitor.Validation
{
    internal class NullableNullRule<T, TValue> : PropertyValidationRuleBase<T, TValue?>
        where TValue : struct
    {
        public NullableNullRule()
        {
            MustExpression = value => !value.HasValue;
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must be null, but found not null";
        }
    }
}