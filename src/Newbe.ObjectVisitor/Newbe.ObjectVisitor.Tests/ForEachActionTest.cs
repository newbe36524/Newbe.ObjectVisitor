using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class ForEachActionTest
    {
        [Test]
        public void FullContext()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .ForEach(c => sb.Append($"{c.Name}:{c.Value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatString();
                    re.Should().Be(except);
                });
        }

        [Test]
        public void FullContextWithExt()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .WithExtendObject(sb)
                .ForEach(c => c.ExtendObject.Append($"{c.Name}:{c.Value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatString();
                    re.Should().Be(except);
                });
        }

        [Test]
        public void NameAndValue()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .ForEach((name, value) => sb.Append($"{name}:{value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatString();
                    re.Should().Be(except);
                });
        }

        [Test]
        public void NameAndValueWithExt()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .WithExtendObject(sb)
                .ForEach((name, value, s) => s.Append($"{name}:{value}{Environment.NewLine}"));

            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatString();
                    re.Should().Be(except);
                });
        }
    }
}