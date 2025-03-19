using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ButtonChanger : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        private Coroutine _coroutine;
        private readonly WaitForSeconds _showTime = new WaitForSeconds(14f);
        private readonly WaitForSeconds _delay = new WaitForSeconds(180f);

        private void OnEnable()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            _coroutine = StartCoroutine(ChangeButton());
        }

        private IEnumerator ChangeButton()
        {
            int i = Random.Range(0, _buttons.Count - 1);

            while (true)
            {
                if (i == _buttons.Count)
                {
                    i = 0;
                }

                yield return _showTime;
                Button currentButton = _buttons[i];
                currentButton.gameObject.SetActive(true);

                yield return _showTime;

                if (currentButton.gameObject.activeSelf)
                {
                    currentButton.gameObject.SetActive(false);

                }

                i++;
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
}

