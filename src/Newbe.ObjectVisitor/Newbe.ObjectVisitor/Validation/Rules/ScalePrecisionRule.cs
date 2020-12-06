namespace Newbe.ObjectVisitor.Validation
{
    internal class ScalePrecisionRule<T, TValue> : PropertyValidationRuleBase<T, TValue>
    {
        public ScalePrecisionRule(
            int scale,
            int precision)
        {
            MustExpression = value =>
                GetScale((decimal) (object) value!) <= scale &&
                GetPrecision((decimal) (object) value!) - GetScale((decimal) (object) value!) <= precision - scale;
            ErrorMessageExpression = (input, value, p) =>
                $"Value of {p.Name} must not be more than {precision} digits in total, with allowance for {scale} decimals. {GetPrecision((decimal) (object) value!)} digits and {GetScale((decimal) (object) value!)} decimals were found";
        }


        #region Copy from https: //github.com/FluentValidation/FluentValidation/blob/55286dd4966dfb75a3c1aaad7813e52a1d312d2e/src/FluentValidation/Validators/ScalePrecisionValidator.cs

        private static uint[] GetBits(decimal @decimal)
        {
            // We want the integer parts as uint
            // C# doesn't permit int[] to uint[] conversion, but .NET does. This is somewhat evil...
            return (uint[]) (object) decimal.GetBits(@decimal);
        }

        private static decimal GetMantissa(decimal @decimal)
        {
            var bits = GetBits(@decimal);
            return (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];
        }

        private static uint GetUnsignedScale(decimal @decimal)
        {
            var bits = GetBits(@decimal);
            uint scale = (bits[3] >> 16) & 31;
            return scale;
        }

        private static int GetScale(decimal @decimal)
        {
            uint scale = GetUnsignedScale(@decimal);
            return (int) scale;
        }

        private static int GetPrecision(decimal @decimal)
        {
            // Precision: number of times we can divide by 10 before we get to 0
            uint precision = 0;
            for (decimal tmp = GetMantissa(@decimal); tmp >= 1; tmp /= 10)
            {
                precision++;
            }

            return (int) precision;
        }

        #endregion
    }
}