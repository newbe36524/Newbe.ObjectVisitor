using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class ExpressionHelperTest
    {
        [Test]
        public void GetPropertyInfo()
        {
            var ageP = typeof(ChildClass).GetPropertyByName(nameof(ChildClass.Age));
            Validate<SuperClass, int>(x => x.ChildClass.Age, ageP);
            Validate<SuperClass, int>(x => x.ChildClasses[0].Age, ageP);

            var nameP = typeof(SuperClass).GetPropertyByName(nameof(SuperClass.Name));
            Validate<SuperClass, string>(x => x.Name, nameP);

            void Validate<T, TValue>(Expression<Func<T, TValue>> source, PropertyInfo propertyInfo)
            {
                var info = source.GetPropertyInfo();
                info.Should().BeSameAs(propertyInfo);
            }
        }

        public class SuperClass
        {
            public string Name { get; set; }
            public ChildClass ChildClass { get; set; }
            public ChildClass[] ChildClasses { get; set; }
        }

        public class ChildClass
        {
            public int Age { get; set; }
        }
    }
}