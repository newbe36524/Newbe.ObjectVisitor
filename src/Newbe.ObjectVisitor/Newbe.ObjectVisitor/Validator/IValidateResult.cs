namespace Newbe.ObjectVisitor.Validator
{
    public interface IValidateResult<out T>
    {
        T Source { get; }
        string[] Errors { get; }
        bool Success { get; }
    }
}