using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator
{
    public class PropertyValidationRuleBuilder<T, TValue> : Newbe.ObjectVisitor.IFluentApi
        , PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
        , PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_V
    {
        private Expression<Func<T, TValue>> _propertyExp = null!;
        private PropertyInfo _propertyInfo = null!;
        private ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S _context;
        private ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_V _contextV;

        public PropertyValidationRuleBuilder(ValidationRuleGroupBuilder<T>.IValidationRuleGroupBuilder_S context)
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
            var mustExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
            _contextV = _context.Validate(mustExp);
        }


        private void Core_ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            var errorMessageExp = Expression.Lambda<Func<T, string>>(bodyExp, pExp);
            _contextV = _contextV.ErrorMessage(errorMessageExp);
        }


        private void Core_If(Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            var pExp = Expression.Parameter(typeof(T), "input");
            var valueExp = Expression.Invoke(_propertyExp, pExp);
            var propertyInfoExp = Expression.Constant(_propertyInfo);
            var bodyExp = Expression.Invoke(func, pExp, valueExp, propertyInfoExp);
            var ifExp = Expression.Lambda<Func<T, bool>>(bodyExp, pExp);
            _context = _context.If(ifExp);
        }


        private void Core_Next()
        {
            _context = _contextV.Next();
        }


        private PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S Core_Property<TNewValue>(
            Expression<Func<T, TNewValue>> propertyExpression)
        {
            var ruleSet = Core_GetRuleSet();
            var builder = new ValidationRuleGroupBuilder<T>(ruleSet).GetBuilder();
            return new PropertyValidationRuleBuilder<T, TNewValue>(builder)
                .GetBuilder(propertyExpression);
        }


        private List<ValidationRuleGroup<T>> Core_GetRuleSet()
        {
            return _context.GetRuleSet();
        }


        private Expression<Func<T, TValue>> Core_GetPropertyExpression()
        {
            return _propertyExp;
        }

        #endregion

        #region AutoGenerate

        public IPropertyValidationRuleBuilder_S GetBuilder(Expression<Func<T, TValue>> propertyExpression)
        {
            Core_GetBuilder(propertyExpression);
            return this;
        }


        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilder_S.Validate(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IPropertyValidationRuleBuilder_V IPropertyValidationRuleBuilder_V.ErrorMessage(
            Expression<Func<T, TValue, PropertyInfo, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.If(
            Expression<Func<T, TValue, PropertyInfo, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_V.Next()
        {
            Core_Next();
            return this;
        }


        PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S IPropertyValidationRuleBuilder_S.
            Property<TNewValue>(Expression<Func<T, TNewValue>> propertyExpression)
        {
            return Core_Property<TNewValue>(propertyExpression);
        }


        List<ValidationRuleGroup<T>> IPropertyValidationRuleBuilder_S.GetRuleSet()
        {
            return Core_GetRuleSet();
        }


        Expression<Func<T, TValue>> IPropertyValidationRuleBuilder_S.GetPropertyExpression()
        {
            return Core_GetPropertyExpression();
        }


        public interface IPropertyValidationRuleBuilder_S
        {
            IPropertyValidationRuleBuilder_S If(Expression<Func<T, TValue, PropertyInfo, bool>> func);


            IPropertyValidationRuleBuilder_V Validate(Expression<Func<T, TValue, PropertyInfo, bool>> func);


            PropertyValidationRuleBuilder<T, TNewValue>.IPropertyValidationRuleBuilder_S Property<TNewValue>(
                Expression<Func<T, TNewValue>> propertyExpression);


            List<ValidationRuleGroup<T>> GetRuleSet();


            Expression<Func<T, TValue>> GetPropertyExpression();
        }


        public interface IPropertyValidationRuleBuilder_V
        {
            IPropertyValidationRuleBuilder_S Next();


            IPropertyValidationRuleBuilder_V ErrorMessage(Expression<Func<T, TValue, PropertyInfo, string>> func);
        }

        #endregion
    }
}