namespace Newbe.ObjectVisitor.Tpl
{
    public class StepInterfaceMethodCodeTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string MethodName { get; set; }
        public string ArgsList { get; set; }

        public string Template { get; } = @"
[Returning] [MethodName]([ArgsList]);
";

        public string Format()
        {
            return CodeTemplateReplacer<StepInterfaceMethodCodeTpl>.GetContent(this);
        }
    }
}