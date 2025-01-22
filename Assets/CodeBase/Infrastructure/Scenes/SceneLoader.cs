using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Scenes
{
    public class SceneLoader
    {
        public const string GAMEPLAY_SCENE = "Gameplay";
        public const string EMPTY_SCENE = "Empty";

        public IReadOnlyReactiveProperty<EScene> CurentScene => _curentScene;
        private ReactiveProperty<EScene> _curentScene = new(EScene.Boot);

        [Inject] private ZenjectSceneLoader _sceneLoader;

        public async UniTask LoadGameplay(
            bool forceReload = false,
            Action<float> progressCallback = null,
            float sceneActivationDelay = 0)
        {
            if (_curentScene.Value == EScene.Gameplay && !forceReload)
            {
                Debug.LogError("Already on CityBuilder Scene");
                return;
            }

            await LoadScene(EScene.Gameplay, GAMEPLAY_SCENE, sceneActivationDelay, progressCallback);
        }

        private async UniTask LoadScene(
            EScene scene,
            string sceneName,
            float sceneActivationDelay,
            Action<float> progressCallback,
            Action<DiContainer> extraBindings = null)
        {
            if (progressCallback == null)
                await LoadSceneWithoutProgress(scene, sceneName, sceneActivationDelay, extraBindings);
            else
                await LoadSceneWithProgress(scene, sceneName, sceneActivationDelay, progressCallback, extraBindings);
        }


        private async UniTask LoadSceneWithoutProgress(
            EScene scene,
            string sceneName,
            float sceneActivationDelay,
            Action<DiContainer> extraBindings)
        {
            await _sceneLoader.LoadSceneAsync(EMPTY_SCENE, LoadSceneMode.Single);
            var operation = _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single, extraBindings);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                    break;
                await UniTask.NextFrame();
            }

            if (sceneActivationDelay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(sceneActivationDelay));
            _curentScene.Value = scene;
            operation.allowSceneActivation = true;
            await operation;
        }

        private async UniTask LoadSceneWithProgress(
            EScene scene,
            string sceneName,
            float sceneActivationDelay,
            Action<float> progressCallback,
            Action<DiContainer> extraBindings)
        {
            progressCallback(0);

            await _sceneLoader.LoadSceneAsync(EMPTY_SCENE, LoadSceneMode.Single)
                .AsObservable(progress: new Progress<float>(pr => progressCallback(pr * 0.2f)));

            var operation = _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single, extraBindings);
            operation.allowSceneActivation = false;

            var fake = FakeProgress(pr => progressCallback(0.2f + 0.65f * pr), 1.5f);

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                    break;
                await UniTask.NextFrame();
            }

            await fake;
            progressCallback(1);

            if (sceneActivationDelay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(sceneActivationDelay));
            _curentScene.Value = scene;
            operation.allowSceneActivation = true;
            await operation;
        }

        private async UniTask FakeProgress(Action<float> progress, float duration)
        {
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                progress(Mathf.Clamp01(time / duration));
                await UniTask.NextFrame();
            }
        }

    }
}
