using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Newbe.ObjectVisitor.Validator
{
    public class Validator<T> : IValidator<T>
    {
        private readonly ValidationRule<T>[] _ruleSet;
        private readonly Expression<Func<T, IValidateResult<T>>> _bodyExp;
        private readonly Func<T, IValidateResult<T>> _func;

        public Validator(
            IEnumerable<ValidationRule<T>> ruleSet)
        {
            _ruleSet = ruleSet.ToArray();
            var inputExp = Expression.Parameter(typeof(T), "input");
            var errorExp = Expression.Variable(typeof(HashSet<string>), "errors");
            var block = Expression.Block(new[] {errorExp}, BlockItems());
            _bodyExp = Expression.Lambda<Func<T, IValidateResult<T>>>(block, inputExp);
            _func = _bodyExp.Compile();

            IEnumerable<Expression> BlockItems()
            {
                yield return Expression.Assign(errorExp, Expression.New(typeof(HashSet<string>)));
                foreach (var rule in _ruleSet)
                {
                    yield return Expression.IfThen(Expression.Not(Expression.Invoke(rule.MustExpression, inputExp)),
                        Expression.Call(errorExp, nameof(HashSet<string>.Add), Array.Empty<Type>(),
                            Expression.Invoke(rule.ErrorMessageExpression, inputExp)));
                }

                var constructorInfo = typeof(ValidateResult<T>).GetTypeInfo().DeclaredConstructors.Single();
                yield return Expression.New(constructorInfo, inputExp, errorExp);
            }
        }

        public IValidateResult<T> Validate(T value)
        {
            var re = _func.Invoke(value);
            return re;
        }
    }
}