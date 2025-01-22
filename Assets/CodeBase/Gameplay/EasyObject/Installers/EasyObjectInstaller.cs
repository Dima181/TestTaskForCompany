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

namespace Gameplay.EasyObject.Installers
{
    [RequireComponent(typeof(Rigidbody))]
    public class EasyObjectInstaller : MonoInstaller
    {
        private Rigidbody _rigidbody;

        public override void InstallBindings()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Container
                .BindInterfacesAndSelfTo<EasyObjectPresenter>()
                .AsSingle();

            Container
            .Bind<Rigidbody>()
            .FromComponentOn(gameObject)
            .AsSingle();
        }
    }

    public class EasyObjectPresenter : IGameObjectInitializable, IGameObjectDisposable
    {
        private readonly ObjectModel _model;
        private readonly Rigidbody _rigidbody;

        private CompositeDisposable _disposable;

        public EasyObjectPresenter(
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
