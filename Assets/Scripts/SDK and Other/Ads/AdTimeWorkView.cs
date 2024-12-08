using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdTimeWorkView : MonoBehaviour
{
    [SerializeField] private ButtonChanger _buttonChanger;
    private float _changeSpeed;
    private Coroutine _coroutine;
    private Image _slider;
    private float _catchTime = 1;

    public void StartShowAdTimeWork(Image image, float increaseTime)
    {
        if (_coroutine != null)
        {
            StopDisplayCatchingTime();
        }

        _coroutine = StartCoroutine(DisplayCatchingTime(image , increaseTime));
    }

    public void StopDisplayCatchingTime()
    {
        _slider.fillAmount = 0;
        StopCoroutine(_coroutine);
    }

    private IEnumerator DisplayCatchingTime(Image image, float increaseTime)
    {
        _slider = image;
        _changeSpeed = _catchTime / increaseTime;
        _slider.fillAmount = _catchTime;

        while (_slider.fillAmount != 0)
        {
            _slider.fillAmount = Mathf.MoveTowards(_slider.fillAmount, 0, _changeSpeed * Time.deltaTime);

            yield return null;
        }
        _buttonChanger.gameObject.SetActive(true);

        _buttonChanger.RestartChangingBottonWithDelay();
    }
}
