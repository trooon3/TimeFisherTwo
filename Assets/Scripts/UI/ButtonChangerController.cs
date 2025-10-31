using Assets.Scripts.FishResources;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ButtonChangerController : MonoBehaviour
    {
        [SerializeField] private BagAdBoostController _adBoostController;
        [SerializeField] private ResourcesManager _resourcesManager;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Rod _rod;
        [SerializeField] private ButtonChanger _buttonChanger;

        public void SetButtonChangerOff()
        {
            _buttonChanger.gameObject.SetActive(false);
        }

        public void SetButtonChangerOn()
        {
            if (!_resourcesManager.IsActiveIncreaseAd && !_mover.IsActiveIncreaseAd && !_adBoostController.IsBoostActive && !_rod.IsActiveIncreaseAd)
            {
                _buttonChanger.gameObject.SetActive(true);
            }
        }
    }
}

