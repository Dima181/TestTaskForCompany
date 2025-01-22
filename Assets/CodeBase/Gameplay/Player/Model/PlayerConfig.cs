namespace Gameplay.Player.Model
{
    public class PlayerConfig
    {
        /*public float moveSpeed = 5f; // Скорость движения персонажа
        public float lookSpeed = 2f; // Скорость вращения камеры
        public float gravity = -9.8f; // Сила гравитации*/

        public float MoveSpeed => _moveSpeed;
        public float LookSpeed => _lookSpeed;
        public float Gravity => _gravity;


        private readonly float _moveSpeed;
        private readonly float _lookSpeed;
        private readonly float _gravity;

        public PlayerConfig(
            float moveSpeed, 
            float lookSpeed, 
            float gravity)
        {
            _moveSpeed = moveSpeed;
            _lookSpeed = lookSpeed;
            _gravity = gravity;
        }

    }
}
