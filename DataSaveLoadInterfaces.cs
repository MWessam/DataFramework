namespace DataContainers
{
    namespace DataPersistance
    {
        public interface IDataPersistance
        {
            void NewData();
        }
        public interface ISaveData
        {
            void SaveData(ref DataContainer gameData);
        }
        public interface ILoadData
        {
            void LoadData(DataContainer gameData);
        }
        public interface IResetData
        {
            void NewData();
        }
    }
}