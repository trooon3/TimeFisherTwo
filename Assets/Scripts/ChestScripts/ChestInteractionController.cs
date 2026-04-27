using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.SkinScripts
{
    public class ChestInteractionController : MonoBehaviour
    {
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private ActiveButtonView _buttonView;
        [SerializeField] private ClosetView _view;

        public bool IsPlayerNearby { get; private set; }

        private void OnEnable() => _playerNearbyChecker.PlayerNearby += OnPlayerApproach;
        private void OnDisable() => _playerNearbyChecker.PlayerNearby -= OnPlayerApproach;

        private void OnPlayerApproach(bool isPlayerApproach)
        {
            IsPlayerNearby = isPlayerApproach;

            if (isPlayerApproach)
                _buttonView.SetActiveEImage(true);
            else
                CloseChest();
        }

        public void OpenChest()
        {
            _view.gameObject.SetActive(true);
            _buttonView.SetActiveEImage(false);
        }

        public void CloseChest()
        {
            _view.gameObject.SetActive(false);
            _buttonView.SetActiveEImage(false);
        }
    }
}
