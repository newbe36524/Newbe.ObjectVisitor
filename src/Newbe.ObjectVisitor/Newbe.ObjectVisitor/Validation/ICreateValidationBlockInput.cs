using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public interface ICreateValidationBlockInput
    {
        ParameterExpression InputExpression { get; }
        ParameterExpression ErrorExpression { get; }
    }
}