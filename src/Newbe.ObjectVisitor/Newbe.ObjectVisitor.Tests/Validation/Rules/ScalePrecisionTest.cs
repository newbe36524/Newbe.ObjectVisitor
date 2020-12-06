using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class ScalePrecisionTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1.0, 0, 1, true)]
        [TestCase(1.1, 0, 1, false)]
        [TestCase(1.1, 0, 2, false)]
        [TestCase(1.1, 1, 2, true)]
        [TestCase(1.01, 2, 2, false)]
        [TestCase(1.01, 1, 3, false)]
        [TestCase(1.01, 2, 3, true)]
        public void ScalePrecision(decimal value, int scale, int precision, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>
                .GetBuilder()
                .Property(x => x.Decimal).ScalePrecision(scale, precision)
                .Build();
            var validator = default(TestModel)!
                .V()
                .Validate(rules);
            var result = validator.Validate(new TestModel
            {
                Decimal = value
            });
            ResultShouldBe(result, success);
        }
    }
}