namespace Newbe.ObjectVisitor.Validator.Rules
{
    public static class ScalePrecisionRuleFactory
    {
        public static ScalePrecisionRule<T, TValue> Create<T, TValue>(
            int scale,
            int precision)
        {
            var rule = new ScalePrecisionRule<T, TValue>(
                scale,
                precision);
            return rule;
        }
    }
}