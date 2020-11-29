using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public interface IValidationBlockExpressionFactoryHandler
    {
        Expression Create(ICreateValidationBlockInput input);
    }
}