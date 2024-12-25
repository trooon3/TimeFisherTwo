using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ButtonChangerController : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Closet _closet;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Rod _rod;
        [SerializeField] private ButtonChanger _buttonChanger;

        public void SetButtonChangerOff()
        {
            _buttonChanger.gameObject.SetActive(false);
        }

        public void SetButtonChangerOn()
        {
            if (!_closet.IsActiveIncreaseAd && !_mover.IsActiveIncreaseAd && !_bag.IsActiveIncreaseAd && !_rod.IsActiveIncreaseAd)
            {
                _buttonChanger.gameObject.SetActive(true);
            }
        }
    }
}

