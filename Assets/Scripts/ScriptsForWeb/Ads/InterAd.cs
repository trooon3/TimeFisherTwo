using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class InterAd : MonoBehaviour
    {
        private readonly WaitForSeconds _threeMinutes = new WaitForSeconds(180f);

        private Coroutine _coroutine;
        private bool _isAvalibleAdd;

        private void Start()
        {
            CheckCoroutineIsNull();
        }

        private IEnumerator InterViewer()
        {
            yield return _threeMinutes;
            _isAvalibleAdd = true;
        }

        private void CheckCoroutineIsNull()
        {
            if (_coroutine != null)
            {
                StopCoroutine(InterViewer());
            }

            _coroutine = StartCoroutine(InterViewer());
        }

        public void ShowAd()
        {
            if (_isAvalibleAdd)
            {
                YandexGame.FullscreenShow();
                _isAvalibleAdd = false;
                CheckCoroutineIsNull();
            }
        }
    }
}