namespace Newbe.ObjectVisitor.Tpl
{
    internal interface ICodeTpl
    {
        string Template { get; }
        string Format();
    }
}