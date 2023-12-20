namespace Source.Scripts.Items
{
    public interface ISpawner<out T>
    {
        public T Spawn();
    }
}