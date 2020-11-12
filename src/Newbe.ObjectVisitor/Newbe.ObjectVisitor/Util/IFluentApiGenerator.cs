namespace Newbe.ObjectVisitor
{
    public interface IFluentApiGenerator
    {
        FluentApiGenerationOutput Create(FluentApiGenerationInput input);
    }
}