using Zenject;

namespace WeatherDogs
{
    public class ServerRequestQueueInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ServerRequestQueue>().AsSingle();
        }
    }
}

