using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public interface ICreateValidationBlockInput
    {
        ParameterExpression InputExpression { get; }
        ParameterExpression ErrorExpression { get; }
    }
}