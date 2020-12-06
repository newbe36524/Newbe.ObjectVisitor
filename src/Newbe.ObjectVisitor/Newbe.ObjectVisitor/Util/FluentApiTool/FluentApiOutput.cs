namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Output of a fluent API generation
    /// </summary>
    public class FluentApiOutput
    {
        /// <summary>
        /// File content
        /// </summary>
        public FluentApiFiles FluentApiFiles { get; set; } = null!;

        /// <summary>
        /// Source design of API
        /// </summary>
        public FluentApiDesign FluentApiDesign { get; set; } = null!;
    }
}