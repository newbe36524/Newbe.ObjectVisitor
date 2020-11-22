using System;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public static class ValidationRuleBuilderExtensions
    {
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V Property<T, TValue>(
            this ValidationRuleBuilder<T>.IValidationRuleBuilder_V source,
            Expression<Func<T, TValue>> propertyExpression)
        {
            var re = new PropertyValidationRuleBuilder<T, TValue>(source)
                .GetBuilder(propertyExpression);
            return re;
        }
    }
}