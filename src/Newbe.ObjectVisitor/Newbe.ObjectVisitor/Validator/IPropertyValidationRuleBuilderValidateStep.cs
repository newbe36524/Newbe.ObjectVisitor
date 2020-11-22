using System;
using System.Linq.Expressions;
using System.Reflection;
using Newbe.ObjectVisitor.Validator.Rules;

namespace Newbe.ObjectVisitor.Validator
{
    public interface IPropertyValidationRuleBuilderValidateStep<T, TValue>
    {
        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V Validate(
            Expression<Func<T, TValue, PropertyInfo, bool>> func);

        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V Validate(
            Expression<Func<TValue, bool>> func);

        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V Validate(
            IPropertyValidationRule<T, TValue> rule);
    }
}