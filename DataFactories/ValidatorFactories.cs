using DataStuff;

namespace DesignPatterns
{
    public class FloatValidatorArgs : CalculatorArgs
    {
        public float Value;
        public FloatValidatorArgs(float value, FloatData data) : base(data)
        {
            Value = value;
        }
    }

    public class SafeFloatTransactionValidatorFactory : Factory
    {
        /// <summary>
        /// Make sure that the args provided is atleast derived from FloatValidatorArgs.
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> A SafeFloatTransactionValidator </returns>
        public override IProduct CreateProduct(DataArgs data)
        {
            FloatValidatorArgs args = (FloatValidatorArgs)data;
            return new SafeFloatTransactionValidator((FloatData)args.DataToOperateOn, args.Value);
        }
    }


}