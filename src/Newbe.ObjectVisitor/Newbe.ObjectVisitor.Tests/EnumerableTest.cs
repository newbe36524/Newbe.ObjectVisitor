using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class EnumerableTest
    {
        [Test]
        public void NameAndValue()
        {
            var model = new TestModel
            {
                List1 = new[] {1},
                List2 = new[] {2},
                List3 = new List<int> {3},
                List4 = new HashSet<int> {4}
            };
            var sumBag = new SumBag();
            var visitor = model.V()
                .WithExtendObject(sumBag)
                .ForEach<TestModel, SumBag, IEnumerable<int>>((name, value, bag) => Sum(bag, value),
                    x => x.IsOrImplOf<IEnumerable<int>>())
                .Cache();
            visitor.Run(model, sumBag);
            sumBag.Sum.Should().Be(10);
        }

        [Test]
        public void NameAndValue2()
        {
            var model = new TestModel
            {
                List1 = new[] {1},
                List2 = new[] {2},
                List3 = new List<int> {3},
                List4 = new HashSet<int> {4}
            };
            var sumBag = new SumBag();
            var visitor = model.V()
                .WithExtendObject(sumBag)
                .ForEach<TestModel, SumBag, IEnumerable<int>>(x => Sum(x.ExtendObject, x.Value),
                    x => x.IsOrImplOf<IEnumerable<int>>())
                .Cache();
            visitor.Run(model, sumBag);
            sumBag.Sum.Should().Be(10);
        }

        private static void Sum(SumBag bag, IEnumerable<int> data)
        {
            bag.Sum += data.Sum();
        }

        public class SumBag
        {
            public int Sum { get; set; }
        }

        public class TestModel
        {
            public int[] List1 { get; set; } = null!;
            public IEnumerable<int> List2 { get; set; } = null!;
            public List<int> List3 { get; set; } = null!;
            public HashSet<int> List4 { get; set; } = null!;
        }
    }
}