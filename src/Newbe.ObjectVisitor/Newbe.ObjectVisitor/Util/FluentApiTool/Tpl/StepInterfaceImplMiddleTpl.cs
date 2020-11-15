namespace Newbe.ObjectVisitor.Tpl
{
    public class StepInterfaceImplMiddleTpl : ICodeTpl
    {
        public string Returning { get; set; }
        public string MethodName { get; set; }
        public string Args { get; set; }

        public string Template { get; } = @"
    [MethodName]([Args]);
    return ([Returning])this;
";

        public string Format()
        {
            return CodeTemplateReplacer<StepInterfaceImplMiddleTpl>.GetContent(this);
        }
    }
}