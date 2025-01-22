using Zenject;

namespace Infrastructure.Pipeline.DataProviders
{
    public class DataProvidersInstaller : Installer<DataProvidersInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalDataProvider>()
                .To(x => x.AllNonAbstractClasses().DerivingFrom<ILocalDataProvider>())
                .AsSingle();
        }
    }
}
