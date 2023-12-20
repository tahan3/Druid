using Zenject;

namespace Source.Scripts.Container
{
    public static class DiContainerRef
    {
        public static DiContainer Container
        {
            get => _container;
            set => _container ??= value;
        }

        private static DiContainer _container;
    }
}