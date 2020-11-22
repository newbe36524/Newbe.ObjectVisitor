using System.Collections.Generic;
using FluentAssertions;
using Newbe.ObjectVisitor.Validator;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validator
{
    public class ValidationRuleBuilderTest
    {
        [Test]
        public void Build()
        {
            var builder = new ValidationRuleBuilder<Yueluo>(new List<ValidationRule<Yueluo>>());

            var yueluoName = "yueluo";
            var rules = builder.GetBuilder()
                .Validate(x => x.Name != yueluoName).ErrorMessage(person => "this is not dalao")
                .Validate(x => x.Age < 20)
                .Property(x => x.Name).Validate(name => name == yueluoName)
                .Property(x => x.Level).If((person, level, p) => person.Age < 20).Validate(level => level > 1000)
                .GetRuleSet();
            rules.Count.Should().Be(4);
        }
    }
}