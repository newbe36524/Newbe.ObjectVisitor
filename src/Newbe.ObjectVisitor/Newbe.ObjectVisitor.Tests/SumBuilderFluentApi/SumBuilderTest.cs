using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.SumBuilderFluentApi
{
    public class SumBuilderTest
    {
        [Test]
        public void SumList()
        {
            var sumBuilder = new SumBuilder(new List<int>());
            var re = sumBuilder
                .AddNumber(1)
                .AddNumber(2)
                .AddNumber(3)
                .Sum();
            re.Should().Be(6);
        }

        [Test]
        public void MultipleSumList()
        {
            var builder = new MultipleSumBuilder(new List<List<int>>());
            var re = builder
                .AddNumber(1)
                .AddNumber(2)
                .NextFactor()
                .AddNumber(3)
                .Sum();
            re.Should().Be(9);
        }
    }
}