#pragma warning disable 8618
namespace Newbe.ObjectVisitor.Tpl
{
    internal class StepInterfaceImplMiddleTpl : ICodeTpl
    {
        public string MethodName { get; set; }
        public string Args { get; set; }

        public string Template { get; } = @"
    [MethodName]([Args]);
    return this;
";

        public string Format()
        {
            return CodeTemplateReplacer<StepInterfaceImplMiddleTpl>.GetContent(this);
        }
    }
}