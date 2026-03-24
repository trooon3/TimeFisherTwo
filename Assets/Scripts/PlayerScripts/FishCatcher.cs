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

        private float _elapsedTime = 0;

        private Bag _bag;
        private Fish _fishToCatch;
        private Coroutine _coroutine;

        private FieldOfView _fieldOfView;
        public FieldOfView FieldOfView => _fieldOfView;
        public float ElapsedTime => _elapsedTime;

        public event UnityAction Catched;
        public event UnityAction<Fish> FishFinded;

        private void Start()
        {
            _fieldOfView = GetComponent<FieldOfView>();
            _bag = GetComponent<Bag>();
        }

        public void SetCatchFish(Fish fish)
        {
            _isCanCatchFish = true;
            _fishToCatch = fish;
            FishFinded.Invoke(_fishToCatch);
        }

        public void ResetSettings()
        {
            _isCanCatchFish = false;
            _fishToCatch = null;
            FishFinded.Invoke(_fishToCatch);
            _elapsedTime = 0;
        }

        public void TryFindFish()
        {
            if (_fishToCatch != null)
            {
                _fishToCatch.CatchTimer.SetCatcher(this);
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
                if (_elapsedTime >= _fishToCatch.CatchTime)
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