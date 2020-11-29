using System.Linq.Expressions;

namespace Newbe.ObjectVisitor.Validator
{
    public class CreateValidationBlockInput : ICreateValidationBlockInput
    {
        public ParameterExpression InputExpression { get; set; }
        public ParameterExpression ErrorExpression { get; set; }
    }
}