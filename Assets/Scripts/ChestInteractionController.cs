using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ChestInteractionController : MonoBehaviour
    {
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private ActiveButtonView _buttonView;
        [SerializeField] private ClosetView _view;

        public event UnityAction<bool> PlayerInteractionChanged;
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

            PlayerInteractionChanged?.Invoke(isPlayerApproach);
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
