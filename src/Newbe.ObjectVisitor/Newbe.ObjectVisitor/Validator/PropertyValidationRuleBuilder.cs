using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Newbe.ObjectVisitor.Validator.Rules;

namespace Newbe.ObjectVisitor.Validator
{
    public class PropertyValidationRuleBuilder<T, TValue> : Newbe.ObjectVisitor.IFluentApi
        , PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
        , PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_IfV
    {
        private Expression<Func<T, TValue>> _propertyExp = null!;
        private PropertyInfo _propertyInfo = null!;
        private readonly ValidationRuleBuilder<T>.IValidationRuleBuilder_V _context;
        private Expression<Func<T, bool>>? _ifExp;
        private Expression<Func<T, bool>> _mustExp = null!;
        private Expression<Func<T, string>>? _errorMessageExp;

        public PropertyValidationRuleBuilder(ValidationRuleBuilder<T>.IValidationRuleBuilder_V context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_GetBuilder(Expression<Func<T, TValue>> propertyExpression)
        {
            _propertyExp = propertyExpression;
            _propertyInfo = propertyExpression.GetPropertyInfo();
        }


        private void Core_Validate(Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            _mustExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
        }


        private void Core_Validate(Expression<Func<TValue, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var bodyExp = Expression.Invoke(func, valueExp);
            var funcExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
            _mustExp = funcExp;
        }


        private void Core_Validate(IPropertyValidationRule<T, TValue> rule)
        {
            Core_Validate(rule.MustExpression);
            Core_ErrorMessage(rule.ErrorMessageExpression);
        }

        private void Core_ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            _errorMessageExp = Expression.Lambda<Func<T, string>>(bodyExp, pExp);
        }


        private void Core_If(Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            _ifExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
        }


        private ValidationRuleBuilder<T>.IValidationRuleBuilder_V Core_AddToRuleSet()
        {
            return _context
                .If(_ifExp!)
                .Validate(_mustExp)
                .ErrorMessage(_errorMessageExp!);
        }


        private PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_V Core_Property<TNewValue>(
            Expression<Func<T, TNewValue>> propertyExpression)
        {
            return Core_AddToRuleSet().Property(propertyExpression);
        }


        private List<ValidationRule<T>> Core_GetRuleSet()
        {
            return Core_AddToRuleSet().GetRuleSet();
        }

        #endregion

        #region AutoGenerate

        public IPropertyValidationRuleBuilder_V GetBuilder(Expression<Func<T, TValue>> propertyExpression)
        {
            Core_GetBuilder(propertyExpression);
            return this;
        }

        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilder_V.ErrorMessage(
            Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IPropertyValidationRuleBuilder_IfV IPropertyValidationRuleBuilder_V.If(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilderValidateStep<T, TValue>.Validate(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilderValidateStep<T, TValue>.Validate(
            Expression<Func<TValue, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilderValidateStep<T, TValue>.Validate(
            IPropertyValidationRule<T, TValue> rule)
        {
            Core_Validate(rule);
            return this;
        }


        ValidationRuleBuilder<T>.IValidationRuleBuilder_V IPropertyValidationRuleBuilder_V.AddToRuleSet()
        {
            return Core_AddToRuleSet();
        }


        PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilder_V.
            Property<TNewValue>(Expression<Func<T, TNewValue>> propertyExpression)
        {
            return Core_Property<TNewValue>(propertyExpression);
        }


        List<ValidationRule<T>> IPropertyValidationRuleBuilder_V.GetRuleSet()
        {
            return Core_GetRuleSet();
        }


        public interface IPropertyValidationRuleBuilder_V : IPropertyValidationRuleBuilderValidateStep<T, TValue>
        {
            IPropertyValidationRuleBuilder_IfV If(Expression<Func<T, TValue, PropertyInfo, bool>> func);

            IPropertyValidationRuleBuilder_V ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func);


            ValidationRuleBuilder<T>.IValidationRuleBuilder_V AddToRuleSet();


            PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_V Property<TNewValue>(
                Expression<Func<T, TNewValue>> propertyExpression);


            List<ValidationRule<T>> GetRuleSet();
        }


        public interface IPropertyValidationRuleBuilder_IfV : IPropertyValidationRuleBuilderValidateStep<T, TValue>
        {
        }

        #endregion
    }
}