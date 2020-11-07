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
        public void FullContextWithExtMultiple()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .WithExtendObject(sb)
                .ForEach(c => c.ExtendObject.Append($"{c.Name}:{c.Value}{Environment.NewLine}"))
                .ForEach(c => c.ExtendObject.Append($"{c.Name}:{c.Value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatString() + obj.FormatString();
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
        public void NameAndValueWithType()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .ForEach<Yueluo, string>((name, value) => sb.Append($"{name}:{value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatOnlyString();
                    re.Should().Be(except);
                });
        }

        [Test]
        public void NameAndValueMultiple()
        {
            var obj = Yueluo.Create();
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var context = obj.V()
                .ForEach((name, value) => sb1.Append($"{name}:{value}{Environment.NewLine}"))
                .ForEach((name, value) => sb2.Append($"{name}:{value}{Environment.NewLine}"));
            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var value1 = sb1.ToString();
                    var value2 = sb2.ToString();
                    var except = obj.FormatString();
                    value1.Should().Be(except);
                    value2.Should().Be(except);
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

        [Test]
        public void NameAndValueWithExtWithType()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var context = obj.V()
                .WithExtendObject(sb)
                .ForEach<Yueluo, StringBuilder, string>((name, value, s) =>
                    s.Append($"{name}:{value}{Environment.NewLine}"));

            TestHelper.RunTest(context,
                () =>
                {
                    context.Run();
                    var re = sb.ToString();
                    var except = obj.FormatOnlyString();
                    re.Should().Be(except);
                });
        }
    }
}