namespace Gameplay.Player.Model
{
    public class PlayerModel
    {
        public string Name => _name;
        public string Description => _description;
        public PlayerConfig Config => _config;


        private readonly string _name;
        private readonly string _description;
        private readonly PlayerConfig _config;

        public PlayerModel(
            string name, 
            string description, 
            PlayerConfig config)
        {
            _name = name;
            _description = description;
            _config = config;
        }

    }
}
