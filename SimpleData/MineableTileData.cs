using DataStuff;
namespace MapData
{
    public enum TileCategory
    {
        Ore,
        Obstacle,
        Unbreakable
    }
    public class MineableTileData : Data
    {
        float _timeToBreak;
        TileCategory _type;
        float _weight;
        float _value;
        public float Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                _dataEvent.InvokeDataChanged();
            }
        }
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _dataEvent.InvokeDataChanged();
            }
        }

        public float TimeToBreak
        {
            get
            {
                return _timeToBreak;
            }
            set
            {
                _timeToBreak = value;
                _dataEvent.InvokeDataChanged();
            }
        }

    }
}