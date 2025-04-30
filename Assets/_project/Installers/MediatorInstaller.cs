using Zenject;

namespace WeatherDogs
{
    public class MediatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WindowMediator>().AsSingle();
        }
    }
}

