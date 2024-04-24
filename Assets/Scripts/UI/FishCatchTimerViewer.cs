using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCatchTimerViewer : MonoBehaviour
{
    private Fish _fish;
    [SerializeField] private FishCatcher _cathcer;
    [SerializeField] private Slider _sliderCatchTime;
    [SerializeField] private float _catchPointChangeSpeed;

    private Coroutine _coroutine;
    private Vector3 _offset = new Vector3(0, 1);

    private void Awake()
    {
        _fish = GetComponent<Fish>();
        _sliderCatchTime.maxValue = _fish.Catchtime;
    }

    private void OnEnable()
    {
        _cathcer.ElapsedTimeChanged += StartDisplayCatching;
       _fish.Catched += OnFishCatched;
    }

    private void OnDisable()
    {
        _cathcer.ElapsedTimeChanged -= StartDisplayCatching;
        _fish.Catched -= OnFishCatched;
    }
    public void StartDisplayCatching()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(DisplayCatch());
    }

    public void OnFishCatched()
    {
        _sliderCatchTime.gameObject.SetActive(false);
    }

    private IEnumerator DisplayCatch()
    {
        while (_sliderCatchTime.value != _cathcer.ElapsedTime)
        {
            _sliderCatchTime.value = Mathf.MoveTowards(_sliderCatchTime.value, _cathcer.ElapsedTime, _catchPointChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    void Update()
    {
       _sliderCatchTime.transform.position = Camera.main.WorldToScreenPoint(_fish.transform.position + _offset);
    }
}
