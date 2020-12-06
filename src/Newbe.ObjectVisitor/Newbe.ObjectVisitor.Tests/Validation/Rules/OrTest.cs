using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class OrTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(3, false)]
        [TestCase(9, true)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        public void TestOr(int value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.Int).Or(x => x.IsInRange(0, 2), x => x.IsInRange(9, 10))
                .Property(x => x.Int).Or(x => x.IsInRange(0, 1).IsInRange(1, 2), x => x.IsInRange(9, 10))
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
    }
}