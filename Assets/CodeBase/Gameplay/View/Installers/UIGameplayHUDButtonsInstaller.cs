using Gameplay.View.HUD;
using Zenject;

namespace Gameplay.View.Installers
{
    public class UIGameplayHUDButtonsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIGrabItUpHUDButtonPresenter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<UIGrabItDownHUDButtonPresenter>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
