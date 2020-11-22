using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidationRuleBuilder<T> : Newbe.ObjectVisitor.IFluentApi
        , ValidationRuleBuilder<T>.IValidationRuleBuilder_V
        , ValidationRuleBuilder<T>.IValidationRuleBuilder_IfV
    {
        private readonly List<ValidationRule<T>> _context;
        private Expression<Func<T, bool>>? _if;
        private ValidationRule<T>? _nowRule;

        public ValidationRuleBuilder(List<ValidationRule<T>> context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_GetBuilder()
        {
            // nothing
        }


        private void Core_Validate(Expression<Func<T, bool>> func)
        {
            _nowRule = new ValidationRule<T>
            {
                ConditionExpression = _if,
                MustExpression = func,
                ErrorMessageExpression = (Expression<Func<T, string>>) (x => "Error")
            };
            _context.Add(_nowRule);
            _if = null;
        }


        private void Core_ErrorMessage(Expression<Func<T, string>>? func)
        {
            if (func != null)
            {
                _nowRule!.ErrorMessageExpression = func;
            }
        }

        private void Core_If(Expression<Func<T, bool>> func)
        {
            _if = func;
        }

        private List<ValidationRule<T>> Core_GetRuleSet()
        {
            return _context;
        }

        #endregion

        #region AutoGenerate

        public IValidationRuleBuilder_V GetBuilder()
        {
            Core_GetBuilder();
            return this;
        }


        IValidationRuleBuilder_V IValidationRuleBuilder_V.Validate(Expression<Func<T, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        IValidationRuleBuilder_V IValidationRuleBuilder_V.ErrorMessage(Expression<Func<T, string>> func)
        {
            Core_ErrorMessage(func);
            return this;
        }


        IValidationRuleBuilder_IfV IValidationRuleBuilder_V.If(Expression<Func<T, bool>> func)
        {
            Core_If(func);
            return this;
        }


        IValidationRuleBuilder_V IValidationRuleBuilder_IfV.Validate(Expression<Func<T, bool>> func)
        {
            Core_Validate(func);
            return this;
        }


        List<ValidationRule<T>> IValidationRuleBuilder_V.GetRuleSet()
        {
            return Core_GetRuleSet();
        }


        public interface IValidationRuleBuilder_V
        {
            IValidationRuleBuilder_IfV If(Expression<Func<T, bool>> func);


            IValidationRuleBuilder_V Validate(Expression<Func<T, bool>> func);


            IValidationRuleBuilder_V ErrorMessage(Expression<Func<T, string>> func);


            List<ValidationRule<T>> GetRuleSet();
        }


        public interface IValidationRuleBuilder_IfV
        {
            IValidationRuleBuilder_V Validate(Expression<Func<T, bool>> func);
        }

        #endregion
    }
}