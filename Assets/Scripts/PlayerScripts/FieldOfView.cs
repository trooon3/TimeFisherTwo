using Assets.Scripts.Fishes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof(Player))]
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _angle;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private FishCatcher _fishCatcher;

        private Player _playerRef;
        private Fish _fish;
        private bool _canSeePlayer;
        private WaitForSeconds _wait;

        public float Radius => _radius;
        public float Angle => _angle;
        public bool CanSeePlayer => _canSeePlayer;
        public Fish FishToCatch => _fish;
        public Player PlayerRef => _playerRef;

        public UnityAction FishFinded;

        private void Start()
        {
            _wait = new WaitForSeconds(0.2f);
            _playerRef = GetComponent<Player>();
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            while (true)
            {
                FieldOfViewCheck();
                yield return _wait;
            }
        }

        public void SetFishNull()
        {
            _fish = null;
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            if (rangeChecks.Length != 0)
            {
                if (rangeChecks[0].TryGetComponent(out Fish fish))
                {
                    Vector3 directionToTarget = (fish.transform.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) <= _angle)
                    {
                        _fish = fish;
                        _fishCatcher.SetCatchFish(_fish);
                        _fishCatcher.TryFindFish();
                        _fish.StartChangeTimerValue();
                    }
                    else
                    {
                        _fish = null;
                        _fishCatcher.ResetSettings();
                        _canSeePlayer = false;
                    }
                }
            }
            else
            {
                _fish = null;
            }
        }
    }
}

