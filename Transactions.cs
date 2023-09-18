using System.Collections.Concurrent;
using System.Threading.Tasks;
using Utilities;

namespace DataStuff
{
    namespace Transactions
    {
        public enum TransactionState
        {
            Processing, Comitted, Aborted, Inactive
        }
        /// <summary>
        /// A transaction is simply a way to handle any value changes concurrently and ensures data integrity.
        /// </summary>
        public interface ITransaction
        {
            /// <summary>
            /// Injects calculator that transaction will use
            /// </summary>
            /// <param name="calculator"></param>
            public void InjectCalculator(IDataCalculator calculator);
            TransactionState State { get; set; }
            /// <summary>
            /// Create and initialize transaction.
            /// </summary>
            /// <returns>Returns CompletedTask when it's done so it can be used asynchronously.</returns>
            Task CreateTransaction();
            /// <summary>
            /// If transaction is successful and no error occured, then apply and commit that transaction.
            /// </summary>
            void Commit();
            /// <summary>
            /// Abort transaction and undo any changes we made.
            /// </summary>
            void Rollback();
        }
        /// <summary>
        /// Handles all current transactions of that piece of data.
        /// </summary>
        public class TransactionHandler
        {
            private ConcurrentQueue<ITransaction> _transactionQueue = new();
            /// <summary>
            /// Add transaction to queue and processes it along with any other transactions that were first in queue.
            /// </summary>
            /// <param name="transaction"></param>
            public async void AddTransaction(ITransaction transaction)
            {
                _transactionQueue.Enqueue(transaction);
                await ProcessTransaction();
            }
            /// <summary>
            /// Go through the entire transaction queue and process each transaction.
            /// </summary>
            /// <returns></returns>
            public async Task ProcessTransaction()
            {
                while (_transactionQueue.Count > 0)
                {
                    ITransaction transaction;
                    if (_transactionQueue.TryDequeue(out transaction))
                    {
                        await transaction.CreateTransaction();
                    }
                }
                return;
            }
        }
    }
}