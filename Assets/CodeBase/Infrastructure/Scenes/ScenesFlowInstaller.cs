using Infrastructure.Scenes.Transitions;
using Zenject;

namespace Infrastructure.Scenes
{
    public class ScenesFlowInstaller : Installer<ScenesFlowInstaller>
    {
        private const string BLACK_PATH = "Systems/Black Transition";

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .AsSingle();

            Container.Bind<SceneTransitions>()
                .AsSingle();

            Container.Bind<OutBlackScreenTransitionView>()
                .FromComponentInNewPrefabResource(BLACK_PATH)
                .WithGameObjectName("[BLACK_TRANSITION]")
                .AsSingle()
                .OnInstantiated<OutBlackScreenTransitionView>((ic, o) =>
                {
                    o.ResetAlpha();
                    o.gameObject.SetActive(false);
                });

            Container.BindInterfacesAndSelfTo<NoTransition>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<OutBlackScreenTransition>()
                .AsSingle();
        }
    }
}
