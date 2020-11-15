namespace Newbe.ObjectVisitor.Tpl
{
    public interface ICodeTpl
    {
        string Template { get; }
        string Format();
    }
}