namespace Newbe.ObjectVisitor.Validation
{
    internal class NullableNotNullRule<T, TValue> : PropertyValidationRuleBase<T, TValue?>
        where TValue : struct
    {
        public NullableNotNullRule()
        {
            MustExpression = value => value.HasValue;
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must not be null, but found null";
        }
    }
}