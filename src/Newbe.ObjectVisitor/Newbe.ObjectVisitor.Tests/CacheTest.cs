using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class CacheTest
    {
        [Test]
        public void NoExtendObj()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var visitor = default(Yueluo).V()
                .ForEach((name, value) => sb.Append($"{name}:{value}{Environment.NewLine}"))
                .Cache();

            visitor.Run(obj);
            sb.ToString().Should().Be(obj.FormatString());
        }

        [Test]
        public void WithExtendObj()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var visitor = default(Yueluo)!.V()
                .WithExtendObject<Yueluo, StringBuilder>()
                .ForEach((name, value, s) => s.Append($"{name}:{value}{Environment.NewLine}"))
                .Cache();

            visitor.Run(obj, sb);
            sb.ToString().Should().Be(obj.FormatString());
        }
    }
}