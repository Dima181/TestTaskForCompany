using Gameplay.Model;
using Gameplay.Player.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerPresenter>()
                .AsSingle();
        }
    }

    public class PlayerPresenter : IGameObjectInitializable, IGameObjectDisposable
    {
        private readonly PlayerModel _model;

        private CompositeDisposable _disposable;

        public PlayerPresenter(
            PlayerModel model)
        {
            _model = model;
            Debug.Log(_model.Name);
        }

        public void Dispose() =>
            _disposable?.Dispose();

        public void Initialize()
        {
        }
    }
}
