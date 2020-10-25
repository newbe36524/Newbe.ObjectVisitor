using System;
using System.Text;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class GetDebugInfoTest
    {
        [Test]
        public void Normal()
        {
            var obj = Yueluo.Create();
            var sb = new StringBuilder();
            var info = obj.V().ForEach((name, value) => sb.Append($"{name}:{value}{Environment.NewLine}"))
                .GetDebugInfo();
            Console.WriteLine(info);
        }
    }
}