namespace Newbe.ObjectVisitor
{
    public class FluentApiGenerationInput
    {
        public string Mermaid { get; set; } = null!;
        public string StateChangerType { get; set; } = null!;
        public string StateType { get; set; } = null!;
        public string StartObjectType { get; set; } = null!;
        public string FinalResultType { get; set; } = null!;
        public string Namespace { get; set; } = null!;
    }
}