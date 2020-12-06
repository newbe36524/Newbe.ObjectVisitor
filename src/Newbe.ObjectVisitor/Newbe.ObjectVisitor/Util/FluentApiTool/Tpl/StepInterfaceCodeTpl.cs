#pragma warning disable 8618

namespace Newbe.ObjectVisitor.Tpl
{
    internal class StepInterfaceCodeTpl : ICodeTpl
    {
        public string Name { get; set; }
        public string[] Methods { get; set; }


        public string Template { get; } = @"
public interface [Name]
{
    [Methods]
}
";

        public string Format()
        {
            return CodeTemplateReplacer<StepInterfaceCodeTpl>.GetContent(this);
        }
    }
}