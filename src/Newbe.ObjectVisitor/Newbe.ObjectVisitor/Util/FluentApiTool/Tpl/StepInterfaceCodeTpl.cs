﻿namespace Newbe.ObjectVisitor.Tpl
{
    public class StepInterfaceCodeTpl : ICodeTpl
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