using Gameplay.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.MiddleObject.Installers
{
    [RequireComponent(typeof(Rigidbody))]
    public class MiddleObjectInstaller : MonoInstaller
    {
        public Rigidbody Rigidbody;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<MiddleObjectPresenter>()
                .AsSingle();

            Container
            .Bind<Rigidbody>()
            .FromComponentOn(gameObject)
            .AsSingle();
        }
    }

    public class MiddleObjectPresenter : IGameObjectInitializable, IGameObjectDisposable
    {
        private readonly ObjectModel _model;
        private readonly Rigidbody _rigidbody;

        private CompositeDisposable _disposable;

        public MiddleObjectPresenter(
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
