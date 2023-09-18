using System;
using DataStuff.Transactions;
using DataStuff.DataPersistance;
using DesignPatterns;

namespace DataStuff
{
    /// <summary>
    /// Base class for all kinds of data the project might need.
    /// For example:
    /// We can have health data that can be incremented every time we heal using a calculator
    /// When healed we can fire an event that will view our new health
    /// and we can also handle data persistance for it.
    /// </summary>
    public abstract class Data : IProduct
    {
        public readonly string DataName;
        protected IDataEvent _dataEvent;
        protected IDataCalculator _currentCalculator;
        protected ITransaction _currentTransaction;
        protected TransactionHandler _transactionHandler = new();
        public Data(string name)
        {
            DataName = name;
        }
        public void InjectCalculator(IDataCalculator calculator)
        {
            _currentCalculator = calculator;
        }
        public void InjectTransaction(ITransaction transaction)
        {
            _currentTransaction = transaction;
        }
        public void PerformOperation(DataArgs value)
        {
            _currentCalculator.InjectValue(value);
            _transactionHandler.AddTransaction(_currentTransaction);
        }

        public void Initialize()
        {
            _currentTransaction.InjectCalculator(_currentCalculator);
        }
        public void SubscribeToOnDataChangedEvent(EventHandler<EventArgs> method)
        {
            _dataEvent.OnDataChanged += method;
        }
            
    }

    /// <summary>
    /// Usage: To calculate specific operations using given data.
    /// </summary>
    public interface IDataCalculator : IProduct
    {
        /// <summary>
        /// Perform Calculation on data stored in class that implemented this interface
        /// </summary>
        /// <returns> Bool which signals that calculation was either successful or not</returns>
        bool Calculate();
        
        /// <summary>
        /// If transaction has been confirmed and committed, apply the result from Calculate() to the Data
        /// </summary>
        void ApplyCalculation();
        /// <summary>
        /// Inject value to be calculated with.
        /// </summary>
        /// <param name="args"> Derive from DataArgs</param>
        void InjectValue(DataArgs args);

        void InjectData(Data data);
    }

    /// <summary>
    /// Validates calculations from IDataCalculator()
    /// </summary>
    public interface IDataValidator : IProduct
    {
        /// <summary>
        /// Validates Calculator output
        /// </summary>
        /// <returns>a bool that determines whether the calculation was valid or not </returns>
        bool Validate();
    }

    /// <summary>
    /// Event that's called everytime the data is called or the data is changed.
    /// </summary>
    public interface IDataEvent
    {
        event EventHandler<EventArgs> OnDataChanged;
        void InvokeDataChanged();
    }

    /// <summary>
    /// Empty class that can be derived from to model the arguments you would like to pass
    /// into some of the Data methods.
    /// </summary>
    public class DataArgs
    {
        /// <summary>
        /// Empty args just incase u didnt need to provide any additional arguments.
        /// </summary>
        public static DataArgs Empty = new DataArgs();
    }
}