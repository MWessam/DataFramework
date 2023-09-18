using DataStuff;
using System;
using System.Collections.Generic;
namespace DesignPatterns
{
    public class CalculatorArgs : DataArgs
    {
        public Data DataToOperateOn;
        public CalculatorArgs(Data data)
        {
            DataToOperateOn = data;
        }
    }
    public class SafeFloatTransactionCalculatorFactory : Factory
    {
        /// <summary>
        /// Make sure that the args provided is atleast derived from CalculatorArgs.
        /// 
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> A SafeFloatTransactionCalculator </returns>
        public override IProduct CreateProduct(DataArgs data)
        {

            return new SafeFloatTransactionCalculator();
        }
    }
    public class FloatAdditionCalculatorFactory : Factory
    {
        /// <summary>
        /// Make sure that the args provided is atleast derived from CalculatorArgs.
        /// 
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> A FloatAdditionCalculator </returns>
        public override IProduct CreateProduct(DataArgs data)
        {
            return new FloatAdditionCalculator();
        }
    }
}


