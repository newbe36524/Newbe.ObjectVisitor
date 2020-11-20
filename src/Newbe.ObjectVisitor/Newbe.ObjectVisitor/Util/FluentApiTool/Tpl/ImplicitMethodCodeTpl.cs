#pragma warning disable 8618
namespace Newbe.ObjectVisitor.Tpl
{
    public class ImplicitMethodCodeTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string InterfaceName { get; set; }
        public string MethodName { get; set; }
        public string ArgsList { get; set; }
        public string Impl { get; set; }

        public string Template { get; } = @"
[Returning] [InterfaceName].[MethodName]([ArgsList])
{
    [Impl]
}
";

        public string Format()
        {
            return CodeTemplateReplacer<ImplicitMethodCodeTpl>.GetContent(this);
        }
    }
}