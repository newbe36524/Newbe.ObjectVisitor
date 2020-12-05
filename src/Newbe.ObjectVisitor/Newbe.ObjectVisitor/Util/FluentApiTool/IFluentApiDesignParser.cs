namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Fluent API design parser
    /// </summary>
    public interface IFluentApiDesignParser
    {
        /// <summary>
        /// Parse a design content to <see cref="FluentApiDesign"/>
        /// </summary>
        /// <param name="designContent">Content of API design</param>
        /// <returns></returns>
        FluentApiDesign Parse(string designContent);
    }
}