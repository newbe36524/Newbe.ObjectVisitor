namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Object visitor factory
    /// </summary>
    public interface IOvFactory
    {
        /// <summary>
        /// Create a object visitor from <paramref name="builderContext"/>
        /// </summary>
        /// <param name="builderContext">Context of object visitor</param>
        /// <returns>Object visitor</returns>
        IObjectVisitor Create(IOvBuilderContext builderContext);
    }
}