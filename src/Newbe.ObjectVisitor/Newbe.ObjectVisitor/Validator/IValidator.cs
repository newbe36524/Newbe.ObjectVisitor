namespace Newbe.ObjectVisitor.Validator
{
    public interface IValidator<T>
    {
        IValidateResult<T> Validate(T value);
    }
}