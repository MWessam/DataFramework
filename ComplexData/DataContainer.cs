using DataStuff;
using DataContainers.DataPersistance;
using DesignPatterns;
using System;
using System.Collections.Generic;

namespace DataContainers
{
    public class DataContainer : ISaveData, ILoadData, IResetData
    {
        private Dictionary<string, Data> _dataMap = new();
        public DataContainer(params Data[] data)
        {
            foreach(Data dataItem in data)
            {
                AddData(dataItem);
            }
        }
        private void AddData(Data data)
        {
            if (_dataMap.ContainsKey(data.DataName))
            {
                _dataMap[data.DataName] = data;
                    
            }
            else
            {
                _dataMap.Add(data.DataName, data);
            }
        }
        public Data GetData(string key)
        {
            return _dataMap[key];
        }
        public void CopyData(DataContainer data)
        {
            _dataMap.Clear();
            _dataMap = data._dataMap;
        }
        // The following methods encapsulate data creation as it can be pretty complicated.
        public void AddFloatData(string name, float MaxValue, float Multiplier = 1.0f)
        {
            AddData(
                (Data)FactoryManager.GetFactory(typeof(FloatDataFactory)).CreateProduct(
                    new DataProductArgs(
                        name, typeof(FloatAdditionCalculator), typeof(BasicDataTransaction), new FloatDataArgs(MaxValue, Multiplier))));

        }
        public void AddSafeFloatData(string name, float MaxValue, float Multiplier = 1.0f)
        {
            AddData(
                (Data)FactoryManager.GetFactory(typeof(FloatDataFactory)).CreateProduct(
                    new DataProductArgs(
                        name, typeof(SafeFloatTransactionCalculator), typeof(BasicDataTransaction), new FloatDataArgs(MaxValue, Multiplier))));
        }

        public void SaveData(ref DataContainer gameData)
        {
            throw new NotImplementedException();
        }

        public void LoadData(DataContainer gameData)
        {
            throw new NotImplementedException();
        }

        public void NewData()
        {
            throw new NotImplementedException();
        }
    }
    
}


