using System;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class FormatStringTest
    {
        [Test]
        public void Normal()
        {
            var obj = Yueluo.Create();
            var re = obj.FormatToString();
            Console.WriteLine(re);
            var except = obj.FormatString();
            re.Should().Be(except);
        }
    }
}