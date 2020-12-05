namespace Newbe.ObjectVisitor.Validation
{
    /// <summary>
    /// Validation result
    /// </summary>
    /// <typeparam name="T">Type of source object</typeparam>
    public interface IValidationResult<out T>
    {
        /// <summary>
        /// Source object
        /// </summary>
        T Source { get; }

        /// <summary>
        /// Error messages
        /// </summary>
        string[] Errors { get; }

        /// <summary>
        /// Validation success
        /// </summary>
        bool Success { get; }
    }
}