using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterAd : MonoBehaviour
{
    private Coroutine _coroutine;
    private WaitForSeconds _threeMinutes = new WaitForSeconds(180f);
    private bool _isAvalibleAdd;

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
            var videoAd = new VideoAd();
            videoAd.ShowInter();
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
