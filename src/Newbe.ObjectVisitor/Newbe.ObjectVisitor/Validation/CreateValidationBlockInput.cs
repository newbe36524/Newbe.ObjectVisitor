using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validation
{
    public class CreateValidationBlockInput : ICreateValidationBlockInput
    {
        public ParameterExpression InputExpression { get; set; }
        public ParameterExpression ErrorExpression { get; set; }
    }
}