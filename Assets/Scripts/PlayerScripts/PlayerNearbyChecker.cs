using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerNearbyChecker : MonoBehaviour
    {
        public bool IsPlayerNearby { get; private set; }
        private Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                IsPlayerNearby = true;
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                IsPlayerNearby = false;
                _player = null;
            }
        }

        public Player GetPlayer()
        {
            return _player;
        }
    }
}

