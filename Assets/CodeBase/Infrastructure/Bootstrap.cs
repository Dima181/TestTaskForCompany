using Cysharp.Threading.Tasks;
using Infrastructure.Pipeline.DataProviders;
using Infrastructure.Scenes;
using Infrastructure.Scenes.Transitions;
using Infrastructure.View;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrap : IInitializable
    {
        [Inject] private readonly BootstrapView _view;
        [Inject] private readonly SceneLoader _sceneLoader;

        [Inject] private readonly OutBlackScreenTransition _blackTransition;

        public async void Initialize()
        {
            void ApplyProgress(float progress) =>
                _view.SetProgress(progress);

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            GameObject.DontDestroyOnLoad(_view.gameObject);
            ApplyProgress(0);

            var di = ProjectContext.Instance.Container;
            var disposableManager = di.Resolve<DisposableManager>();

            ApplyProgress(0.1f);

            await InitializeData(di, disposableManager, pr =>
                ApplyProgress(0.1f + pr * 0.85f));


            await _blackTransition.ApplyTransition(async () =>
            {
                await _sceneLoader.LoadGameplay();

                GameObject.Destroy(_view.gameObject);
            });
        }

        private async UniTask InitializeData(
            DiContainer di,
            DisposableManager disposableManager,
            Action<float> progress)
        {
            progress(0);
            var temp = di.CreateSubContainer();

            DataProvidersInstaller.Install(temp);
            progress(0.1f);

            var localProviders = temp.ResolveAll<ILocalDataProvider>();
            progress(0.2f);

            var count = localProviders.Count;
            var cur = 0;
            progress(0.25f);

            foreach (var provider in localProviders)
            {
                var instanse = await provider.Load(di, disposableManager);
                di.Bind(provider.ModelType)
                    .FromInstance(instanse)
                    .AsSingle();
                progress(0.25f + 0.7f * (++cur) / count);
            }

            temp.UnbindAll();
            progress(1);
        }
    }
}