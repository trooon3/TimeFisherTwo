using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCatchTimerViewer : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private Slider _sliderCatchTime;
    private FishCatcher _cathcer;
    private float _catchPointChangeSpeed = 1f;

    private Coroutine _coroutine;

    private void OnDisable()
    {
        _cathcer.ElapsedTimeChanged -= StartDisplayCatching;
    }

    public void SetCatcher(FishCatcher catcher)
    {
        _cathcer = catcher;
        _cathcer.ElapsedTimeChanged += StartDisplayCatching;
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

    private IEnumerator DisplayCatch()
    {
        if (_cathcer == null)
        {
            _sliderCatchTime.value = 0;
            yield return null;
        }

        while (_sliderCatchTime.value != _cathcer.ElapsedTime)
        {
            _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value, _cathcer.ElapsedTime, _catchPointChangeSpeed * Time.deltaTime);

            if (_sliderCatchTime.value == 0)
            {
                _cathcer = null;
            }

            yield return null;
        }
    }
}
