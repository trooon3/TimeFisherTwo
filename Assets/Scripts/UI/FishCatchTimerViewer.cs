using Assets.Scripts.Fishes;
using Assets.Scripts.PlayerScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FishCatchTimerViewer : MonoBehaviour
    {
        [SerializeField] private Fish _fish;
        [SerializeField] private Slider _sliderCatchTime;
        [SerializeField] private float _catchPointChangeSpeed;
        [SerializeField] private Image _filledBag;

        private FishCatcher _cathcer;
        private FieldOfView _fieldOfView;
        private Coroutine _coroutine;
        private Coroutine _bagCoroutine;
        private readonly WaitForSeconds _showTime = new WaitForSeconds(5f);

        private void Update()
        {
            if (_cathcer == null)
            {
                _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value,
                    0, _catchPointChangeSpeed * Time.deltaTime);
                ShowFilledBag(false);
            }
        }

        public void SetCatcher(FishCatcher catcher)
        {
            if (_cathcer == null)
            {
                _cathcer = catcher;
                _fieldOfView = _cathcer.FieldOfView;
            }
        }

        public void StartDisplayCatching()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (_fish.gameObject.activeSelf)
            {
                _coroutine = StartCoroutine(DisplayCatch());
            }
        }

        public void ResetValue()
        {
            _sliderCatchTime.value = 0;
        }

        public void ShowFilledBag(bool active)
        {
            _filledBag.gameObject.SetActive(active);

            if (_bagCoroutine != null)
            {
                StopCoroutine(_bagCoroutine);
            }

            if (_fish.gameObject.activeSelf)
            {
                _bagCoroutine = StartCoroutine(ShowBagFilledDelay());
            }
        }

        private IEnumerator DisplayCatch()
        {
            while (_sliderCatchTime.value != _cathcer.ElapsedTime)
            {
                if (_cathcer != null)
                {
                    _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value,
                        _cathcer.ElapsedTime, _catchPointChangeSpeed * Time.deltaTime);
                }

                if (_sliderCatchTime.value == _cathcer.ElapsedTime)
                {
                    _fieldOfView.SetFishNull();
                }

                yield return null;
            }
        }

        private IEnumerator ShowBagFilledDelay()
        {
            if (_filledBag.gameObject.activeSelf)
            {
                yield return _showTime;
            }

            _filledBag.gameObject.SetActive(false);
        }
    }
}

