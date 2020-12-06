using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal interface IValidationBlockExpressionFactoryHandler
    {
        Expression Create(ICreateValidationBlockInput input);
    }
}