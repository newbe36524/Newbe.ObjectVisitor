namespace Newbe.ObjectVisitor.Tests.ConnectionStringBuilderFluentApi
{
    public class ConnectionStringModel
    {
        public string Host { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsWindowsAuthentication { get; set; }
    }
}