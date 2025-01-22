using Cysharp.Threading.Tasks;
using Gameplay.Installers;
using Gameplay.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class ObjectSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameplayObject _prefab;

        [Inject] private ObjectsCollection _collection;
        [Inject] private DiContainer _di;
        private IDisposable _disposable;

        private void OnEnable()
        {
            var building = _collection.Get(_prefab.Id);

            _disposable = building.CurrentMass
                .AsObservable()
                .Where(mass => mass > 0)
                .Subscribe(state => Construct(building).Forget());
        }

        private void OnDisable() =>
            _disposable?.Dispose();

        private async UniTaskVoid Construct(ObjectModel model)
        {
            var subContainer = _di.CreateSubContainer();

            subContainer.Bind<ObjectModel>()
                .FromInstance(model)
                .AsSingle();

            Debug.Log(_prefab.Id);

            var installer = subContainer.InstantiatePrefabForComponent<GameplayObject>(
                _prefab,
                transform.position,
                transform.rotation,
                transform.parent);

            installer.name = model.Name;
            subContainer.Inject(installer);
            installer.InstallBindings();

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
