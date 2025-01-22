using Gameplay.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.HeavyObject.Installers
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeavyObjectInstaller : MonoInstaller
    {
        public Rigidbody Rigidbody;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<HeavyObjectPresenter>()
                .AsSingle();

            Container
            .Bind<Rigidbody>()
            .FromComponentOn(gameObject)
            .AsSingle();
        }
    }

    public class HeavyObjectPresenter : IGameObjectInitializable, IGameObjectDisposable
    {
        private readonly ObjectModel _model;
        private readonly Rigidbody _rigidbody;

        private CompositeDisposable _disposable;

        public HeavyObjectPresenter(
            ObjectModel model,
            Rigidbody rigidbody)
        {
            _model = model;
            _rigidbody = rigidbody;
            Debug.Log(_model.Name);
        }

        public void Dispose() =>
            _disposable?.Dispose();

        public void Initialize() =>
            _rigidbody.mass = _model.Config.Mass.Value;
    }
}
