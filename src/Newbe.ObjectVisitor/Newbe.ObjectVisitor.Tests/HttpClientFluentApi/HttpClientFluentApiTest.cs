using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.HttpClientFluentApi
{
    public class HttpClientFluentApiTest
    {
        [Test]
        public void BuildPost()
        {
            var builder = new RequestBuilder(new HttpRequestMessage());
            var message = builder.Post()
                .SetUri(new Uri("https://www.newbe.pro"))
                .SetContent(new StringContent("yueluo, the only one dalao"))
                .Build();
            message.Should().NotBeNull();
        }

        [Test]
        public void BuildGet()
        {
            var builder = new RequestBuilder(new HttpRequestMessage());
            var message = builder.Get()
                .SetUri(new Uri("https://www.newbe.pro"))
                .Build();
            message.Should().NotBeNull();
        }
    }
}