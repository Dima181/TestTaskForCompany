using Gameplay.Player.Model;
using SimpleInputNamespace;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [Inject]
        private void Construct(PlayerModel model)
        {
            _moveSpeed = model.Config.MoveSpeed;
            _lookSpeed = model.Config.LookSpeed;
            _gravity = model.Config.Gravity;
        }

        public Transform CameraTransform;
        
        private float _moveSpeed;
        private float _lookSpeed;
        private float _gravity;

        private CharacterController _characterController;
        private Vector3 _velocity;
        private float _cameraPitch = 0f;


        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            if (CameraTransform == null)
                Debug.LogError("Камера не назначена! Перетащите камеру в поле 'Camera Transform' в инспекторе.");
        }

        private void Update()
        {
            HandleMovement();
            HandleLook();
        }

        private void HandleMovement()
        {
            // Получаем данные от первого джойстика для перемещения
            float moveX = SimpleInput.GetAxis("Horizontal"); // Влево/вправо
            float moveY = SimpleInput.GetAxis("Vertical");   // Вперед/назад

            // Создаем вектор движения в локальных координатах
            Vector3 moveDirection = transform.right * moveX + transform.forward * moveY;

            // Применяем гравитацию
            if (_characterController.isGrounded)
            {
                _velocity.y = 0f; // Сбрасываем вертикальную скорость, если на земле
            }
            else
            {
                _velocity.y += _gravity * Time.deltaTime;
            }

            // Перемещаем персонажа
            _characterController.Move((moveDirection * _moveSpeed + _velocity) * Time.deltaTime);
        }

        private void HandleLook()
        {
            // Получаем данные от второго джойстика для вращения камеры
            float lookX = SimpleInput.GetAxis("RightJoystickHorizontal"); // Вращение по горизонтали
            float lookY = SimpleInput.GetAxis("RightJoystickVertical");   // Вращение по вертикали

            // Вращаем персонажа по горизонтали
            transform.Rotate(Vector3.up * lookX * _lookSpeed);

            // Наклоняем камеру по вертикали (ограничиваем угол наклона)
            _cameraPitch -= lookY * _lookSpeed;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f); // Ограничиваем наклон

            if (CameraTransform != null)
            {
                CameraTransform.localEulerAngles = new Vector3(_cameraPitch, 0f, 0f);
            }
        }
    }
}
