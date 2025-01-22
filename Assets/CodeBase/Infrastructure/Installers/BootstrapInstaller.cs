using Infrastructure;
using Infrastructure.View;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>()
                .AsSingle();

            Container.Bind<BootstrapView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}