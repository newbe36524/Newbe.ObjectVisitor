using System;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class DictionaryTest
    {
        [Test]
        public void ToDic()
        {
            //TODO Does not meet expectations
            var obj = Yueluo.Create();
            var dic = obj.V().CollectAsDictionary();
            Console.WriteLine(dic);
            
        }
    }
}