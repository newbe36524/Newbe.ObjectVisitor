using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
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
            public NoFlagEnum NoFlagEnum { get; set; }
            public byte NoFlagEnumByte { get; set; }
            public string NoFlagEnumString { get; set; }
            public FlagsEnum FlagsEnum { get; set; }
            public int FlagsEnumInt { get; set; }
            public string FlagsEnumString { get; set; }
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

        public enum NoFlagEnum
        {
            Spring = 0,
            Summer = 1,
            Autumn = 3,
            Winter = 4
        }

        [Flags]
        public enum FlagsEnum
        {
            Morning = 0x01,
            Afternoon = 0X02,
            Evening = 0x04,
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
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.Int).IsInRange(min, max, excludeMin, excludeMax)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
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
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.Int).IsInSet(dataRange)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
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
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.String).Length(min, max)
                .Property(x => x.Ints).Length(min, max)
                .Property(x => x.EnumerableItem).Length(min, max)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
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
                result.Errors.Length.Should().Be(3);
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }

        [Test]
        [TestCase((NoFlagEnum) 5, 5, "???", false)]
        [TestCase(NoFlagEnum.Autumn, 3, nameof(NoFlagEnum.Autumn), true)]
        public void NoFlagEnumTest(NoFlagEnum enumValue, byte byteValue, string stringValue, bool success)
        {
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.NoFlagEnum).IsInEnum()
                .Property(x => x.NoFlagEnumByte).IsInEnum(typeof(NoFlagEnum))
                .Property(x => x.NoFlagEnumString).IsInEnum(typeof(NoFlagEnum))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NoFlagEnum = enumValue,
                NoFlagEnumByte = byteValue,
                NoFlagEnumString = stringValue
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                result.Errors.Length.Should().Be(3);
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }

        [Test]
        [TestCase((FlagsEnum) 0, 0, "0", false)]
        [TestCase(FlagsEnum.Afternoon | FlagsEnum.Evening, 3, nameof(FlagsEnum.Evening), true)]
        public void FlagsEnumTest(FlagsEnum enumValue, int intValue, string stringValue, bool success)
        {
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.FlagsEnum).IsInEnum()
                .Property(x => x.FlagsEnumInt).IsInEnum(typeof(FlagsEnum))
                .Property(x => x.FlagsEnumString).IsInEnum(typeof(FlagsEnum))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                FlagsEnum = enumValue,
                FlagsEnumInt = intValue,
                FlagsEnumString = stringValue
            });
            result.Success.Should().Be(success);
            if (!success)
            {
                result.Errors.Length.Should().Be(3);
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(3, false)]
        [TestCase(9, true)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        public void OrTest(int value, bool success)
        {
            var rules = ValidateRule<TestModel>.GetBuilder()
                .Property(x => x.Int).Or(x => x.IsInRange(0, 2), x => x.IsInRange(9, 10))
                .Property(x => x.Int).Or(x => x.IsInRange(0, 1).IsInRange(1, 2), x => x.IsInRange(9, 10))
                .Property(x => x.Int).Not(x =>
                    x.Or(a => a.LessThan(0), a => a.IsInRange(2, 9), a => a.GreaterThanOrEqual(10)))
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = value,
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