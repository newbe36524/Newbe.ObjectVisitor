using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validation
{
    internal class Validator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidationBlockExpressionFactoryHandler> _factories;
        private Expression<Func<T, IValidationResult<T>>> _bodyExp = null!;
        private Func<T, IValidationResult<T>> _func = null!;

        public Validator(
            IEnumerable<ValidationRuleGroup<T>> ruleGroups)
        {
            IValidationBlockExpressionFactory factory = new ValidationBlockExpressionFactory();
            _factories = ruleGroups.Select(x => factory.Create(x));
            InitValidator();
        }

        private void InitValidator()
        {
            var inputExp = Expression.Parameter(typeof(T), "input");
            var errorExp = Expression.Variable(typeof(HashSet<string>), "errors");
            var block = Expression.Block(new[] {errorExp}, BlockItems());
            _bodyExp = Expression.Lambda<Func<T, IValidationResult<T>>>(block, inputExp);
            _func = _bodyExp.Compile();

            IEnumerable<Expression> BlockItems()
            {
                yield return Expression.Assign(errorExp, Expression.New(typeof(HashSet<string>)));
                foreach (var factory in _factories)
                {
                    var blockExp = factory.Create(new CreateValidationBlockInput
                    {
                        ErrorExpression = errorExp,
                        InputExpression = inputExp
                    });
                    yield return blockExp;
                }

                var constructorInfo = typeof(ValidationResult<T>).GetTypeInfo().DeclaredConstructors.Single();
                yield return Expression.New(constructorInfo, inputExp, errorExp);
            }
        }

        public IValidationResult<T> Validate(T value)
        {
            var re = _func.Invoke(value);
            return re;
        }
    }
}