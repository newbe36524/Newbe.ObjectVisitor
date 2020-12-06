using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class LessThanTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, false)]
        [TestCase(2, 1, false)]
        public void TestLessThan(int value, int max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Int).LessThan(max)
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
        [TestCase(1, 1, false)]
        [TestCase(2, 1, false)]
        [TestCase(null, 1, false)]
        public void TestLessThanNullable(int? value, int max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).LessThan(max)
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
        [TestCase(1, 1, false)]
        [TestCase(2, 1, false)]
        [TestCase(null, 1, true)]
        [TestCase(1, null, false)]
        [TestCase(null, null, false)]
        public void TestLessThanComparer(int? value, int? max, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.NullInt).LessThan(max, new IntNullableComparer())
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