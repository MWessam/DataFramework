using System.Threading.Tasks;
namespace DataContainers
{
    namespace DataPersistance
    {
        public enum LoadType
        {
            New,
            Save,
            Load
        }
        public static class DataPersistanceManager
        {
            static DataContainer _savedData;
            static DataContainer _dataToSave;
            static string _fileName = "Data.json";
            static FileDataHandler _dataHandler;
            public static LoadType LoadingType = LoadType.New;

            public static void CreateSaveFile(string path, string fileName)
            {
                _fileName = fileName;
                _dataHandler = new FileDataHandler(path, _fileName);
                _savedData = new DataContainer();
            }
            public static void Start(DataContainer dataToSave)
            {
                _dataToSave = dataToSave;
                switch (LoadingType)
                {
                    case LoadType.New:
                        NewGame();
                        break;
                    case LoadType.Save:
                        SaveGame(); 
                        break;
                    case LoadType.Load:
                        LoadGame();
                        break;
                }
            }

            public static void NewGame()
            {
                _dataHandler.Reset();
                _savedData.NewData();
                _dataToSave.NewData();
            }

            public static void LoadGame()
            {
                _savedData = _dataHandler.Load();
                if (_savedData == null)
                {
                    NewGame();
                }
                _dataToSave.LoadData(_savedData);
            }

            public static async Task SaveGame()
            {
                _dataToSave.SaveData(ref _savedData);
                await _dataHandler.Save(ref _savedData);
            }
        }
    }
}

