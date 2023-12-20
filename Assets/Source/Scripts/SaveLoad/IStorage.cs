using Unity.VisualScripting;

namespace Source.Scripts.SaveLoad
{
    public interface IStorage<TData> : IDataLoader, IDataSaver
    {
        public void Add(TData data);
        public void Remove(TData data);
    }
}