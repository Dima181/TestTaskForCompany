using Gameplay.Model;
using System.Runtime.CompilerServices;

namespace Gameplay.Player.Model
{
    public class PlayerBuilder
    {
        private string _name;
        private PlayerConfig _config;

        public PlayerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PlayerBuilder WithConfig(PlayerConfig config)
        {
            _config = config;
            return this;
        }

        public PlayerModel Build()
        {
            return new PlayerModel(
                _name,
                "Description",
                _config);
        }
    }
}
