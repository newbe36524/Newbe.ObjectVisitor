using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public interface IValidationBlockExpressionFactoryHandler
    {
        Expression Create(ICreateValidationBlockInput input);
    }
}