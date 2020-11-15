namespace Newbe.ObjectVisitor.Tpl
{
    public class StepInterfaceImplFinalTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string MethodName { get; set; }
        public string Args { get; set; }

        public string Template { get; } = @"
    [Returning] [MethodName]([Args]);
";

        public string Format()
        {
            return CodeTemplateReplacer<StepInterfaceImplFinalTpl>.GetContent(this);
        }
    }
}