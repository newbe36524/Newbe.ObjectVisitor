#pragma warning disable 8618
namespace Newbe.ObjectVisitor.Tpl
{
    public class PublicMethodCodeTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string MethodName { get; set; }
        public string ArgsList { get; set; }
        public string Impl { get; set; }

        public string Template { get; } = @"
public [Returning] [MethodName]([ArgsList])
{
    [Impl]
}
";

        public string Format()
        {
            return CodeTemplateReplacer<PublicMethodCodeTpl>.GetContent(this);
        }
    }
}