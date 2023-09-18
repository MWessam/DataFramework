using DataStuff;

namespace DesignPatterns
{
    public class BasicDataTransactionFactory : Factory
    {
        /// <summary>
        /// Args can be any args but it's not gonna be used anyways, so just use DataArgs.Empty.
        /// </summary>
        /// <param name="data"> </param>
        /// <returns> A BasicDataTransaction </returns>
        public override IProduct CreateProduct(DataArgs data)
        {
            return new BasicDataTransaction();
        }
    }
}