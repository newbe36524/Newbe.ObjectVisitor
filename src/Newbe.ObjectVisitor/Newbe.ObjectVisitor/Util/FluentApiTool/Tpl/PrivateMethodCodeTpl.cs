#pragma warning disable 8618
namespace Newbe.ObjectVisitor.Tpl
{
    public class PrivateMethodCodeTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string MethodName { get; set; }
        public string ArgsList { get; set; }
        public string Impl { get; set; }

        public string Template { get; } = @"
private [Returning] [MethodName]([ArgsList])
{
    [Impl]
}
";

        public string Format()
        {
            return CodeTemplateReplacer<PrivateMethodCodeTpl>.GetContent(this);
        }
    }
}