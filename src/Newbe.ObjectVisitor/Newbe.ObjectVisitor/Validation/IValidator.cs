namespace Newbe.ObjectVisitor.Validation
{
    public interface IValidator<T>
    {
        IValidationResult<T> Validate(T value);
    }
}