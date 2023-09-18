using System.Threading.Tasks;
using DataStuff.DataPersistance;
using DataStuff.Transactions;
using DesignPatterns;

namespace DataStuff
{
    /// <summary>
    /// Represents all data that can be represented as float that has a minimum and a maximum value.
    /// Such as: Health, Fuel, etc
    /// </summary>
    [System.Serializable]
    public class FloatData : Data
    {
        private float _currentFloatValue;
        private Stat _maxFloatValue;
        public Stat MaxFloatValue { get => _maxFloatValue; }
        public float CurrentFloatValue
        {
            get
            {
                return _currentFloatValue;
            }
            set
            {
                _currentFloatValue = value;
                _dataEvent.InvokeDataChanged();
            }
        }
        public FloatData(Stat maxFloatValue, string name) : base(name) 
        {
            _maxFloatValue = maxFloatValue;
            CurrentFloatValue = _maxFloatValue.CurrentStat;
        }
    }

    /// <summary>
    /// Basic data transaction that handles and validates calculations.
    /// </summary>
    public class BasicDataTransaction : ITransaction, IProduct
    {
        IDataCalculator _calculator;
        public TransactionState State { get; set; }
        public void InjectCalculator(IDataCalculator calculator)
        {
            _calculator = calculator;
        }

        public Task CreateTransaction()
        {
            State = TransactionState.Processing;
            if (_calculator.Calculate())
            {
                Commit();
                return Task.CompletedTask;
            }
            Rollback();
            return Task.CompletedTask;
        }
        public void Commit()
        {
            _calculator.ApplyCalculation();
            State = TransactionState.Comitted;
        }
        public void Rollback()
        {
            State = TransactionState.Aborted;
        }
        public void Initialize()
        {
            State = TransactionState.Inactive;
        }
    }
    /// <summary>
    /// Base class for all float calculations (Increment/Decrement, Clamping, SafeIncrement/SafeDecrement)
    /// </summary>
    public abstract class FloatCalculator : IDataCalculator, IProduct
    {
        protected FloatData _data;
        protected float _result;
        protected FloatCalculatorArgs _valueToPerformCalculationWith;
        

        public void InjectValue(float value)
        {
            _valueToPerformCalculationWith.Value = value;
        }
        public abstract bool Calculate();
        public virtual void ApplyCalculation()
        {
            _data.CurrentFloatValue = _result;
        }

        public void InjectValue(DataArgs args)
        {
            _valueToPerformCalculationWith = (FloatCalculatorArgs)args;
        }

        public void Initialize()
        {

        }

        public void InjectData(Data data)
        {
            if (data is FloatData data1)
            {
                _data = data1;
            }
        }
    }

    /// <summary>
    /// Calculator for safe transactions, I.E Money transactions, where you will only go through with it IF the user has enough.
    /// </summary>
    public class SafeFloatTransactionCalculator : FloatCalculator
    {
        IDataValidator _validator;

        public override bool Calculate()
        {
            _result = _data.CurrentFloatValue + _valueToPerformCalculationWith.Value;
            _validator = new SafeFloatTransactionValidator(_data, _result);
            if (_validator.Validate())
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Adds/Subtracts from the float value and will clamp it to its bounds.
    /// </summary>
    public class FloatAdditionCalculator : FloatCalculator
    {
        public override bool Calculate()
        {            
            _result = _data.CurrentFloatValue + _valueToPerformCalculationWith.Value;
            ApplyCalculation();
            return true;
            
        }
        public override void ApplyCalculation()
        {
            Clamp(0.0f, _data.MaxFloatValue.CurrentStat);
            base.ApplyCalculation();
        }
        private void Clamp(float min, float max)
        {
            if (_result < min)
            {
                _result = min;
            }
            else if (_result > max)
            {
                _result = max;
            }
        }
    }

    /// <summary>
    /// Checks whether this operation can be done or not.
    /// </summary>
    public class SafeFloatTransactionValidator : IDataValidator
    {
        float _valueToValidate;
        FloatData _data;
        public SafeFloatTransactionValidator(FloatData data, float valueToValidate)
        {
            _valueToValidate = valueToValidate;
            _data = data;
        }

        public void Initialize()
        {

        }

        public bool Validate()
        {
            return _valueToValidate >= 0.0f && _valueToValidate <= _data.MaxFloatValue.CurrentStat;
        }
    }
    public class FloatDataArgs : DataArgs
    {
        public float MaxValue;
        public float Multiplier;
        public FloatDataArgs(float maxValue, float multiplier)
        {
            MaxValue = maxValue;
            Multiplier = multiplier;
        }
    }

    public class FloatCalculatorArgs : DataArgs
    {
        public float Value;
        public FloatCalculatorArgs(float value)
        {
            Value = value;
        }
    }
}