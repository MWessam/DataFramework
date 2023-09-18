using DataStuff;
using DataStuff.Transactions;
using System;
using System.Transactions;
using Utilities;

namespace DesignPatterns
{
    public class DataProductArgs : DataArgs
    {
        public Type CalculatorType;
        public Type TransactionType;
        public DataArgs DataInitializationValues;
        public string Name;
        public DataProductArgs(string name, Type calculatorType, Type transactionType, DataArgs dataInitializationValues)
        {
            Name = name;
            CalculatorType = calculatorType;
            TransactionType = transactionType;
            DataInitializationValues = dataInitializationValues;
        }
    }
    public abstract class DataFactory : Factory
    {
        protected Data _data;
        protected IDataCalculator _calculator;
        protected ITransaction _transaction;
        public override IProduct CreateProduct(DataArgs data)
        {
            DataProductArgs givenArgs = (DataProductArgs)data;
            CreateDependencies(givenArgs);
            CreateData(givenArgs.DataInitializationValues, givenArgs.Name);
            InitializeData();
            return _data;
        }
        protected abstract void CreateData(DataArgs args, string name);

        private void CreateDependencies(DataProductArgs data)
        {
            _calculator = (IDataCalculator)FactoryManager.GetFactory(data.CalculatorType).CreateProduct(DataArgs.Empty);
            _transaction = (ITransaction)FactoryManager.GetFactory(data.TransactionType).CreateProduct(DataArgs.Empty);
        }
        private void InitializeData()
        {
            _calculator.InjectData(_data);
            _data.InjectTransaction(_transaction);
            _data.InjectCalculator(_calculator);
            _data.Initialize();
        }
    }

    public class FloatDataFactory : DataFactory
    {
        protected override void CreateData(DataArgs args, string name)
        {
            FloatDataArgs floatDataArgs = (FloatDataArgs)args;
            _data = new FloatData(new Stat(floatDataArgs.MaxValue, floatDataArgs.Multiplier), name);
        }
    }
}