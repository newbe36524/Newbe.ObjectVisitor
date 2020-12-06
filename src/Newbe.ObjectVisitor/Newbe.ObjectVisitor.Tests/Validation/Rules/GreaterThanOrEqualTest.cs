using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class GreaterThanOrEqualTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        public void TestGreaterThanOrEqual(int value, int min, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).GreaterThanOrEqual(min)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = value,
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        [TestCase(null, 1, false)]
        public void TestGreaterThanOrEqualNullable(int? value, int min, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).GreaterThanOrEqual(min)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NullInt = value,
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        [TestCase(null, 1, false)]
        [TestCase(1, null, true)]
        [TestCase(null, null, true)]
        public void TestGreaterThanOrEqualComparer(int? value, int? min, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).GreaterThanOrEqual(min, new IntNullableComparer())
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                NullInt = value,
            });
            ResultShouldBe(result, success);
        }
    }
}