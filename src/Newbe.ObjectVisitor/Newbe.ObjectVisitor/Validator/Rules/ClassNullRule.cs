namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class ClassNullRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : class
    {
        public ClassNullRule()
        {
            MustExpression = value => value == default(TValue);
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must be null, but found not null";
        }
    }
}