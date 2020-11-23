using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validator
{
    public class ValidateRuleTest
    {
        #region TestModel

        public class TestModel
        {
            public int Int { get; set; }
            public string String { get; set; }
            public int[] Ints { get; set; }
            public IEnumerable<int> Items { get; set; }
            public EnumerableItem EnumerableItem { get; set; }
        }

        public class EnumerableItem : IEnumerable
        {
            private readonly int _count;

            public EnumerableItem(
                int count)
            {
                _count = count;
            }

            public IEnumerator GetEnumerator()
            {
                return new Enumerator(_count);
            }

            private class Enumerator : IEnumerator
            {
                private readonly int _count;
                private int _now = -1;

                public Enumerator(int count)
                {
                    _count = count;
                }

                public bool MoveNext()
                {
                    if (_now + 1 < _count)
                    {
                        _now++;
                        return true;
                    }

                    return false;
                }

                public void Reset()
                {
                    _now = 0;
                }

                public object Current => _now;
            }
        }

        #endregion

        [Test]
        [TestCase(1, 2, false, true, false)]
        [TestCase(1, 2, false, false, true)]
        [TestCase(1, 3, false, true, true)]
        [TestCase(2, 3, true, true, false)]
        [TestCase(2, 3, false, true, true)]
        public void IsInRange(int min, int max, bool excludeMin, bool excludeMax, bool success)
        {
            var builder = new ValidationRuleBuilder<TestModel>(new List<ValidationRule<TestModel>>());
            var validationRules = builder.GetBuilder()
                .Property(x => x.Int).IsInRange(min, max, excludeMin, excludeMax)
                .GetRuleSet();
            var validator = new Validator<TestModel>(validationRules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                Console.WriteLine(result.Errors.Single());
            }
        }

        [Test]
        [TestCase(new[] {1, 2, 3}, true)]
        [TestCase(new[] {1, 3}, false)]
        [TestCase(new int[0], false)]
        public void IsInSet(int[] dataRange, bool success)
        {
            var builder = new ValidationRuleBuilder<TestModel>(new List<ValidationRule<TestModel>>());
            var validationRules = builder.GetBuilder()
                .Property(x => x.Int).IsInSet(dataRange)
                .GetRuleSet();
            var validator = new Validator<TestModel>(validationRules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                Console.WriteLine(result.Errors.Single());
            }
        }

        [Test]
        [TestCase(0, 1, false)]
        [TestCase(2, 4, true)]
        [TestCase(3, 4, false)]
        public void Length(int min, int max, bool success)
        {
            var builder = new ValidationRuleBuilder<TestModel>(new List<ValidationRule<TestModel>>());
            var validationRules = builder.GetBuilder()
                .Property(x => x.String).Length(min, max)
                .Property(x => x.Ints).Length(min, max)
                .Property(x => x.EnumerableItem).Length(min, max)
                .GetRuleSet();
            var validator = new Validator<TestModel>(validationRules);
            var result = validator.Validate(new TestModel
            {
                Ints = new[] {1, 2},
                String = "12",
                Items = new List<int> {1, 2},
                EnumerableItem = new EnumerableItem(2)
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}