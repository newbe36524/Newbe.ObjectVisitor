namespace Newbe.ObjectVisitor.Validation
{
    internal class StringNotEmptyRule<T> : PropertyValidationRuleBase<T, string>
    {
        public StringNotEmptyRule()
        {
            MustExpression = value => !string.IsNullOrWhiteSpace(value);
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must be empty, but found null or empty";
        }
    }
}