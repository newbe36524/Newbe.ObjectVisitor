#pragma warning disable 8618
namespace Newbe.ObjectVisitor
{
    public class ApiStep
    {
        public string SourceContent { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Action { get; set; }
        public string Share { get; set; }
        public string Return { get; set; }
        public bool IsStart => From == "[*]";
        public bool IsEnd => To == "[*]";
        public bool ContainsShare => !string.IsNullOrEmpty(Share);
        public bool ContainsReturn => !string.IsNullOrEmpty(Return);

        public override string ToString()
        {
            return SourceContent;
        }
    }
}