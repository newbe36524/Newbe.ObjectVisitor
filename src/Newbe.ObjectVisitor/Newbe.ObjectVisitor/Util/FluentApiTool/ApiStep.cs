#pragma warning disable 8618
namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Definition of API step
    /// </summary>
    public class ApiStep
    {
        /// <summary>
        /// Source design content
        /// </summary>
        public string SourceContent { get; set; }

        /// <summary>
        /// State name of action beginning
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// State name of action ending
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// State moving action name
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Indicate that <see cref="Action"/> share a logic as <see cref="Share"/>
        /// </summary>
        /// <example>
        /// <code>
        /// e.g. 1
        /// A --&gt; B : C()
        /// there will be a action named C generated.
        /// A --&gt; B : C() :shared D()
        /// there will be a action named C generated. It is not user custom function, and it invoke D in it`s block and function D is a user custom function
        /// </code>
        /// </example>
        public string Share { get; set; } = null!;

        /// <summary>
        /// Indicate that the returning type of this action. It is often used in final action.
        /// </summary>
        public string Return { get; set; } = null!;

        /// <summary>
        /// This action is a beginning action
        /// </summary>
        public bool IsStart => From == "[*]";

        /// <summary>
        /// This action is a final action
        /// </summary>
        public bool IsEnd => To == "[*]";

        /// <summary>
        /// Is this action contains share block or not
        /// </summary>
        public bool ContainsShare => !string.IsNullOrEmpty(Share);

        /// <summary>
        /// Is this action contains return block or not
        /// </summary>
        public bool ContainsReturn => !string.IsNullOrEmpty(Return);

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SourceContent;
        }
    }
}