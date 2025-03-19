using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerNearbyChecker : MonoBehaviour
    {
        private Player _player;
        public bool IsPlayerNearby { get; private set; }
        public UnityAction PlayerNearby;
        public UnityAction PlayerFar;

        public Player GetPlayer()
        {
            return _player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                IsPlayerNearby = true;
                PlayerNearby?.Invoke();
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                IsPlayerNearby = false;
                PlayerFar?.Invoke();
                _player = null;
            }
        }
    }
}

