namespace Newbe.ObjectVisitor
{
    public interface IFluentApiFileGenerator
    {
        FluentApiOutput Generate(FluentApiDesign design);
    }
}