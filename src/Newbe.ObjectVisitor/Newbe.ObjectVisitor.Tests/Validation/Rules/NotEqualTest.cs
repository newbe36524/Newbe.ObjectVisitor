using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class NotEqualTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        public void NotEqual(int value, int expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).NotEqual(expected)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Int = value
            });
            ResultShouldBe(result, success);
        }

        [Test]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        [TestCase(null, 1, true)]
        [TestCase(null, null, false)]
        public void NotEqualNullable(int? value, int? expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).NotEqual(expected)
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
        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        [TestCase(null, 1, true)]
        [TestCase(null, null, false)]
        public void NotEqualComparer(int? value, int? expected, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).NotEqual(expected, new IntNullableEqualityComparer())
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