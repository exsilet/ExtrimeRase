namespace SaveData
{
    public interface IDataProvider
    {
        bool TryLoad();
        void Save();
    }
}