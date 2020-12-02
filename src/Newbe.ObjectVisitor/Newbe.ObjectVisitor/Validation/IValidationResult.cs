namespace Newbe.ObjectVisitor.Validation
{
    public interface IValidationResult<out T>
    {
        T Source { get; }
        string[] Errors { get; }
        bool Success { get; }
    }
}