using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonChanger : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    private WaitForSeconds _showTime = new WaitForSeconds(2f);
    private WaitForSeconds _delay = new WaitForSeconds(180f);
    private Coroutine _coroutine;

    private void Start()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(false);
        }

        _coroutine = StartCoroutine(ChangeButton());

    }

    private IEnumerator ChangeButton()
    {
        while (true)
        {
            Button currentButton = _buttons[Random.Range(0, _buttons.Count)];
            currentButton.gameObject.SetActive(true);

            yield return _showTime;
            currentButton.gameObject.SetActive(false);

            yield return _showTime;
        }
    }


    public void RestartChangingBottonWithDelay()
    {
        StopCoroutine(_coroutine);
       _coroutine = StartCoroutine(RestartChangeBottonWithDelay());
    }

    public IEnumerator RestartChangeBottonWithDelay()
    {
        yield return _delay;
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ChangeButton());
    }
}
