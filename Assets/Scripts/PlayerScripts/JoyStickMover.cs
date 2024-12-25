using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class JoyStickMover : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _joystick;

        private PlayerMover _playerMover;
        private Vector3 _direction;
        private Vector3 _stopDirection = new Vector3(0, 0, 0);

        void Start()
        {
            _playerMover = GetComponent<PlayerMover>();
        }

        void Update()
        {
            _direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

            if (_direction != _stopDirection)
            {
                _playerMover.JoyStickMove(_direction);
            }
        }
    }
}

