namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Object validator
    /// </summary>
    /// <typeparam name="T">Type of object to be validated</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validate a value
        /// </summary>
        /// <param name="value">Value to be validated</param>
        /// <returns></returns>
        IValidationResult<T> Validate(T value);
    }
}