using Assets.Scripts.Fishes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof(FieldOfView))]
    public class FishCatcher : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController _player;

        [Range(0, 360)]
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _radius;
        [SerializeField] private bool _isCanCatchFish;
        [SerializeField] private FishSpawner _spawner;

        private float _angle = 100;
        private float _elapsedTime = 0;
        private int _fishNumber = 9;
        private int _fish;

        private Bag _bag;
        private Fish _fishToCatch;
        private Coroutine _coroutine;

        private FieldOfView _fieldOfView;
        public FieldOfView FieldOfView => _fieldOfView;
        public Fish FishToCatch => _fishToCatch;
        public float ElapsedTime => _elapsedTime;

        public UnityAction Catched;

        private void Start()
        {
            _fieldOfView = GetComponent<FieldOfView>();
            Mathf.Clamp(_angle, _minAngle, _maxAngle);
            _bag = GetComponent<Bag>();
            _fish = 1 << _fishNumber;
        }

        public void SetCatchFish(Fish fish)
        {
            _isCanCatchFish = true;
            _fishToCatch = fish;
        }

        public void ResetSettings()
        {
            _isCanCatchFish = false;
            _fishToCatch = null;
            _elapsedTime = 0;
        }

        public void TryFindFish()
        {
            if (_fishToCatch != null)
            {
                _fishToCatch.SetCatcher(this);
                TryCatchFish();
            }
            else
            {
                if (_elapsedTime != 0)
                {
                    _elapsedTime = 0;
                }
            }
        }

        private void TryCatchFish()
        {
            StartElapseTime();
        }

        private void TryAddFish(Fish fish)
        {
            if (_bag.TryAddFish(fish))
            {
                Catched?.Invoke();
                _spawner.SetOffFish(fish);
            }
        }

        private void StartElapseTime()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (_fishToCatch != null)
            {
                _coroutine = StartCoroutine(ElapseTime());
            }
        }

        private IEnumerator ElapseTime()
        {
            while (_isCanCatchFish)
            {
                if (_elapsedTime >= _fishToCatch.Catchtime)
                {
                    _isCanCatchFish = false;
                    _elapsedTime = 0;
                    TryAddFish(_fishToCatch);
                    _fishToCatch = null;
                }

                _elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}

