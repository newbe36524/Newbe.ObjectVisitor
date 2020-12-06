using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    internal class CreateValidationBlockInput : ICreateValidationBlockInput
    {
        public ParameterExpression InputExpression { get; set; } = null!;
        public ParameterExpression ErrorExpression { get; set; } = null!;
    }
}