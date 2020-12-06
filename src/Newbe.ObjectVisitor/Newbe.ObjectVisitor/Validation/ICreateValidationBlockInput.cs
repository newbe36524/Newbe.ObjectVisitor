using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal interface ICreateValidationBlockInput
    {
        ParameterExpression InputExpression { get; }
        ParameterExpression ErrorExpression { get; }
    }
}