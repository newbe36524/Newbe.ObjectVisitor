using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class LambdaTest
    {
        [Test]
        public void NoExtendObj()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var action = default(Yueluo).V()
                .ForEach((name, value) => sb.Append($"{name}:{value}{Environment.NewLine}"))
                .GetLambda();

            action.Invoke(obj);
            sb.ToString().Should().Be(obj.FormatString());
        }

        [Test]
        public void WithExtendObj()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var action = default(Yueluo)!.V()
                .WithExtendObject<Yueluo, StringBuilder>()
                .ForEach((name, value, s) => s.Append($"{name}:{value}{Environment.NewLine}"))
                .GetLambda();

            action.Invoke(obj, sb);
            sb.ToString().Should().Be(obj.FormatString());
        }
    }
}