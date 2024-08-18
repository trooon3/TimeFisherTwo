using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterAd : MonoBehaviour
{
    private Coroutine _coroutine;
    private WaitForSeconds _twoMinets = new WaitForSeconds(180f);

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
        var videoAd = new VideoAd();
        videoAd.ShowInter();
    }

    private IEnumerator InterViewer()
    {
        while (true)
        {
            ShowAd();
            yield return _twoMinets;
        }
    }
}
