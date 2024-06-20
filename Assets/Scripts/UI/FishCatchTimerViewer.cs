using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCatchTimerViewer : MonoBehaviour
{
    [SerializeField] private Fish _fish;
    [SerializeField] private Slider _sliderCatchTime;
    [SerializeField] private float _catchPointChangeSpeed;
    private FishCatcher _cathcer;
    private FieldOfView _fieldOfView;

    private Coroutine _coroutine;

    public void SetCatcher(FishCatcher catcher)
    {
        if (_cathcer == null)
        {
            _cathcer = catcher;
            _fieldOfView = _cathcer.FieldOfView;
        }
        
    }

    private void Update()
    {
        if (_cathcer == null)
        {
            _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value, 0, _catchPointChangeSpeed * Time.deltaTime);
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

    private IEnumerator DisplayCatch()
    {
        
        Debug.Log("зашли в дисплей кетч");

        while (_sliderCatchTime.value != _cathcer.ElapsedTime)
        {
            if (_cathcer != null)
            {
                
                _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value, _cathcer.ElapsedTime, _catchPointChangeSpeed * Time.deltaTime);

            }

            if (_sliderCatchTime.value == _cathcer.ElapsedTime)
            {
                _fieldOfView.SetFishNull();
            }

            yield return null;
        }
    }
}
