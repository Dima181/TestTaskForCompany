using Cysharp.Threading.Tasks;
using Gameplay.Player.Model;
using Infrastructure.Pipeline.DataProviders;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.Player.Services
{
    public class LocalPlayerDataProvider : LocalSingleDataProvider<PlayerModel>
    {
        protected override async UniTask<PlayerModel> Load(
            DiContainer di, 
            DisposableManager disposableManager)
        {
            await UniTask.CompletedTask;

            return InitializePlayerModel();
        }

        public PlayerModel InitializePlayerModel()
        {
            PlayerConfig stateConfig = new(moveSpeed: 5f, lookSpeed: 2f, gravity: -9.8f);

            return new PlayerBuilder()
                .WithName("Player")
                .WithConfig(stateConfig)
                .Build();
        }
    }
}
