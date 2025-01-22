using Cysharp.Threading.Tasks;
using Gameplay.Installers;
using Gameplay.Model;
using Gameplay.Player.Installers;
using Gameplay.Player.Model;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameplayPlayer _prefab;

        [Inject] private PlayerModel _model;
        [Inject] private DiContainer _di;
        private IDisposable _disposable;

        private bool _isPlayerSpawned;

        private void OnEnable()
        {
            if( !_isPlayerSpawned)
            {
                SpawnPlayer().Forget();
                _isPlayerSpawned = true;
            }
        }

        private async UniTaskVoid SpawnPlayer()
        {
            var subContainer = _di.CreateSubContainer();

            subContainer.Bind<PlayerModel>()
                .FromInstance(_model)
                .AsSingle();

            var playerInstance = subContainer.InstantiatePrefabForComponent<GameplayPlayer>(
                _prefab,
                transform.position,
                transform.rotation,
                transform.parent);

            playerInstance.name = _model.Name;
            subContainer.Inject(playerInstance);
            playerInstance.InstallBindings();

            var initializables = subContainer.ResolveAll<IGameObjectInitializable>();
            var disposables = subContainer.ResolveAll<IGameObjectDisposable>();
            var disposableManager = subContainer.Resolve<DisposableManager>();

            foreach (var initializable in initializables)
                initializable.Initialize();
            foreach (var disposable in disposables)
                disposableManager.Add(disposable);

            await UniTask.NextFrame();
            Destroy(gameObject);
        }
    }
}
