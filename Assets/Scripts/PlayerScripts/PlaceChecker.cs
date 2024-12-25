using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlaceChecker : MonoBehaviour
    {
        public bool IsOnGround { get; private set; }
        public bool InWater { get; private set; }
        public bool IsInteractbleNearby { get; private set; }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out Ground ground))
            {
                IsOnGround = false;

            }

            if (collider.TryGetComponent(out Water water))
            {
                InWater = false;
            }

            if (collider.TryGetComponent(out PlayerNearbyChecker playerNearbyChecker))
            {
                IsInteractbleNearby = true;
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.TryGetComponent(out Ground ground))
            {
                IsOnGround = true;
            }

            if (collider.TryGetComponent(out Water water))
            {
                InWater = true;
            }

            if (collider.TryGetComponent(out PlayerNearbyChecker playerNearbyChecker))
            {
                IsInteractbleNearby = true;
            }
        }
    }
}

