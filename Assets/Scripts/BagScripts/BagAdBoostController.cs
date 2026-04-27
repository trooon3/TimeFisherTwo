using System.Collections;
using UnityEngine;
using Assets.Scripts.UI;

namespace Assets.Scripts.BagScripts
{
    public class BagAdBoostController : MonoBehaviour
    {
        [SerializeField] private float _boostDuration = 60f;
        [SerializeField] private ButtonChangerController _buttonController;
        private Coroutine _boostCoroutine;
        private WaitForSeconds _boostWait;

        public bool IsBoostActive { get; private set; }
        public float BoostDuration => _boostDuration;

        private void Awake() => _boostWait = new WaitForSeconds(_boostDuration);

        public void ActivateBoost()
        {
            IsBoostActive = true;
            _buttonController.SetButtonChangerOff();
            _boostCoroutine = StartCoroutine(BoostTimer());
        }

        private IEnumerator BoostTimer()
        {
            yield return _boostWait;
            IsBoostActive = false;
            _buttonController.SetButtonChangerOn();
        }
    }
}