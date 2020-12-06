using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newbe.ObjectVisitor.Validation;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.Validation
{
    public class ValidationRuleBuilderTest
    {
        [Test]
        public void Build()
        {
            var builder = new ValidationRuleGroupBuilder<Yueluo>(new List<ValidationRuleGroup<Yueluo>>());

            var yueluoName = "yueluo";
            var rules = builder.GetBuilder()
                .Validate(x => x.Name != yueluoName).ErrorMessage(person => "this is not dalao")
                .Validate(x => x.Age < 20)
                .Property(x => x.Name).Validate(name => name == yueluoName)
                .Property(x => x.Level).If((person, level, p) => person.Age < 20).Validate(level => level > 1000)
                .Build();
            rules.Count.Should().Be(3);
        }

        [Test]
        public void Or()
        {
            var builder = new ValidationRuleGroupBuilder<Yueluo>(new List<ValidationRuleGroup<Yueluo>>());

            var yueluoName = "yueluo";
            var rules = builder.GetBuilder()
                .Or(x => x.Validate(a => a.Name != yueluoName), x => x.Validate(a => a.Age < 20))
                .Build();
            rules.Count.Should().Be(1);
            var g = rules.Single();
            g.RuleRelation.Should().Be(ValidationRuleRelation.Or);
        }
    }
}