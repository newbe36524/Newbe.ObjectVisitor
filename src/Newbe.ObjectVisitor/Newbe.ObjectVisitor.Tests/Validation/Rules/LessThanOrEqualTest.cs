using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class LessThanOrEqualTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, false)]
        public void LessThanOrEqual(int value, int max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).LessThanOrEqual(max)
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
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, false)]
        [TestCase(null, 1, false)]
        public void LessThanOrEqualNullable(int? value, int max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).LessThanOrEqual(max)
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
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, false)]
        [TestCase(null, 1, true)]
        [TestCase(1, null, false)]
        [TestCase(null, null, true)]
        public void LessThanOrEqualComparer(int? value, int? max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).LessThanOrEqual(max, new IntNullableComparer())
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