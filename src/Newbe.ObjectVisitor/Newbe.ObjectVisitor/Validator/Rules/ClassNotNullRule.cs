namespace Newbe.ObjectVisitor.Validator.Rules
{
    public class ClassNotNullRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
        where TValue : class
    {
        public ClassNotNullRule()
        {
            MustExpression = value => value != default(TValue);
            ErrorMessageExpression = (input, value, p) => $"Value of {p.Name} must not be null, but found null";
        }
    }
}