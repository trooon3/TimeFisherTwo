using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class InterAd : MonoBehaviour
    {
        private Coroutine _coroutine;
        private bool _isAvalibleAdd;
        private readonly WaitForSeconds _threeMinutes = new WaitForSeconds(180f);

        private void Start()
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
            }
        }

        private IEnumerator InterViewer()
        {
            while (true)
            {
                yield return _threeMinutes;
                _isAvalibleAdd = true;
            }
        }
    }
}

