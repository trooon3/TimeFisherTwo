using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RodCatchViewer : MonoBehaviour
    {
        [SerializeField] private Image _slider;
        [SerializeField] private Image _fishIcon;
        [SerializeField] private Image _happyFaceIcon;
        [SerializeField] private Rod _rod;

        private Coroutine _coroutine;

        private float _changeSpeed;
        private float _catchTime = 1;

        private void Start()
        {
            _fishIcon.gameObject.SetActive(false);
            _happyFaceIcon.gameObject.SetActive(false);
            _slider.fillAmount = 0;
        }

        public void StartDisplayCatchingTime()
        {
            if (_coroutine != null)
            {
                StopDisplayCatchingTime();
            }

            _coroutine = StartCoroutine(DisplayCatchingTime());
        }

        public void StopDisplayCatchingTime()
        {
            _slider.fillAmount = 0;
            StopCoroutine(_coroutine);
        }

        public void SetOffHappyFace()
        {
            _happyFaceIcon.gameObject.SetActive(false);
        }

        private IEnumerator DisplayCatchingTime()
        {
            _happyFaceIcon.gameObject.SetActive(false);
            _fishIcon.gameObject.SetActive(true);
            _changeSpeed = _rod.CatchingSpeed;

            while (_slider.fillAmount != _catchTime)
            {
                _slider.fillAmount = Mathf.MoveTowards(_slider.fillAmount, _catchTime, _changeSpeed * Time.deltaTime);

                if (_slider.fillAmount == _catchTime)
                {
                    _fishIcon.gameObject.SetActive(false);
                    _happyFaceIcon.gameObject.SetActive(true);
                    _rod.GetFishFoodFor();

                }

                yield return null;
            }
        }
    }
}

