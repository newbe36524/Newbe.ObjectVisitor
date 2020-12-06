using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class ValidationRuleTestBase
    {
        #region TestModel

        public class TestModel
        {
            public int Int { get; set; }
            public int? NullInt { get; set; }
            public string String { get; set; }
            public int[] Ints { get; set; }
            public IEnumerable<int> Items { get; set; }
            public EnumerableItem EnumerableItem { get; set; }
            public NoFlagEnum NoFlagEnum { get; set; }
            public byte NoFlagEnumByte { get; set; }
            public string NoFlagEnumString { get; set; }
            public FlagsEnum FlagsEnum { get; set; }
            public int FlagsEnumInt { get; set; }
            public int? FlagsEnumNullableInt { get; set; }
            public string FlagsEnumString { get; set; }
            public decimal Decimal { get; set; }
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

        protected void ResultShouldBe<T>(IValidationResult<T> result, bool success)
        {
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }

            result.Success.Should().Be(success);
        }
    }
}