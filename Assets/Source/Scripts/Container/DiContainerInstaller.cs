using Zenject;

namespace Source.Scripts.Container
{
    public class DiContainerInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;
        
        public override void InstallBindings()
        {
            DiContainerRef.Container = _diContainer;
        }
    }
}