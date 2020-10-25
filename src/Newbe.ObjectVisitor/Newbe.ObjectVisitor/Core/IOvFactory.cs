namespace Newbe.ObjectVisitor
{
    public interface IOvFactory
    {
        IObjectVisitor Create(IOvBuilderContext builderContext);
    }
}