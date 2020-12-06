using Newbe.ObjectVisitor.Validation;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation.Rules
{
    public class NotTest : ValidationRuleTestBase
    {
        [Test]
        [TestCase(1, false)]
        [TestCase(0, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        public void Not(int value, bool success)
        {
            var rules = ValidationRuleBuilder<TestModel>.GetBuilder()
                .Property(x => x.Int).Not(x => x.IsInRange(0, 2))
                .Property(x => x.Int).Or(x => x.LessThan(0), x => x.GreaterThanOrEqual(2))
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