using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class IsInRangeTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1, 2, false, true, false)]
        [TestCase(1, 2, false, false, true)]
        [TestCase(1, 3, false, true, true)]
        [TestCase(2, 3, true, true, false)]
        [TestCase(2, 3, false, true, true)]
        public void IsInRange(int min, int max, bool excludeMin, bool excludeMax, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.Int).IsInRange(min, max, excludeMin, excludeMax)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = 2
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(2, 1, 2, false, true, false)]
        [TestCase(2, 1, 2, false, false, true)]
        [TestCase(2, 1, 3, false, true, true)]
        [TestCase(2, 2, 3, true, true, false)]
        [TestCase(2, 2, 3, false, true, true)]
        [TestCase(null, 2, 3, false, true, false)]
        [TestCase(null, -1, 1, false, true, false)]
        public void NullableIntIsInRange(int? value, int min, int max, bool excludeMin, bool excludeMax, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.NullInt).IsInRange(min, max, excludeMin, excludeMax)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NullInt = value
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(2, 1, 2, false, true, false)]
        [TestCase(2, 1, 2, false, false, true)]
        [TestCase(2, 1, 3, false, true, true)]
        [TestCase(2, 2, 3, true, true, false)]
        [TestCase(2, 2, 3, false, true, true)]
        [TestCase(null, 2, 3, false, true, false)]
        [TestCase(null, -1, 1, false, true, false)]
        public void NullableIntIsInRangeWithComparer(int? value, int min, int max, bool excludeMin, bool excludeMax, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.NullInt).IsInRange(min, max, new IntNullableComparer(), excludeMin, excludeMax)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NullInt = value
            });
            ResultShouldBe(result, success);
        }
    }
}