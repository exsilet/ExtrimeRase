using Zenject;

namespace Services
{
    public sealed class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<PausedGame>().AsSingle();
        }
    }
}