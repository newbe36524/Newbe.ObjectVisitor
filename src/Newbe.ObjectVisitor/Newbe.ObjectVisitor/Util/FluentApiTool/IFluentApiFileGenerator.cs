namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Fluent API file generator
    /// </summary>
    public interface IFluentApiFileGenerator
    {
        /// <summary>
        /// Generate fluent API
        /// </summary>
        /// <param name="design">API design</param>
        /// <returns></returns>
        FluentApiOutput Generate(FluentApiDesign design);
    }
}