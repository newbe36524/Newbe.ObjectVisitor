using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.ConnectionStringBuilderFluentApi
{
    public class ConnectionStringBuilderTest
    {
        [Test]
        public void UseUsernamePassword()
        {
            var builder = new ConnectionStringBuilder(new ConnectionStringModel());
            var re = builder.SetHost("localhost")
                .UseUsernamePassword("yueluo", "dalao")
                .Build();
            re.Should().Be("Host=localhost;Username=yueluo;Password=dalao;");
        }
        
        [Test]
        public void UseWindowsAuthentication()
        {
            var builder = new ConnectionStringBuilder(new ConnectionStringModel());
            var re = builder.SetHost("localhost")
                .UseWindowsAuthentication()
                .Build();
            re.Should().Be("Host=localhost;IsWindowsAuthentication=True;");
        }
    }
}