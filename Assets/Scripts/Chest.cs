using Assets.Scripts.Fishes;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves;
using Assets.Scripts.ScripsForWeb.Ads;
using Assets.Scripts.Tutorial;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private FishSpawner _spawner;
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private ClosetView _view;
        [SerializeField] private ActiveButtonView _buttonView;
        [SerializeField] private RodCatchViewer _rodView;
        [SerializeField] private TutorialViewer _tutorial;
        [SerializeField] private InterAd _interAd;
        [SerializeField] private ResourcesManager _resourcesManager;
        [SerializeField] private FishesManager _fishesManager;

        private void Start()
        {
            YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialRod);

           _view.gameObject.SetActive(true);
           _view.gameObject.SetActive(false);
            OnPlayerApproach(false);
        }

        private void OnEnable()
        {
            _fishesManager.FishTransferred += SaveCounters;
            _resourcesManager.ResourceCountChanged += SaveCounters;
            _playerNearbyChecker.PlayerNearby += OnPlayerApproach;
        }

        private void OnDisable()
        {
            _fishesManager.FishTransferred -= SaveCounters;
            _resourcesManager.ResourceCountChanged -= SaveCounters;
            _playerNearbyChecker.PlayerNearby -= OnPlayerApproach;
        }

        private void TakeFish(List<Fish> fishes)
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                _fishesManager.AddFish(fishes[i]);
                _resourcesManager.AddResources(fishes[i]);
                _spawner.SetOffFish(fishes[i]);
            }
            _tutorial.ShowWhereUpgrade();
        }

        private void OnPlayerApproach(bool isPlayerApproach)
        {
            if (isPlayerApproach)
            {
                _buttonView.SetActiveEImage(true);
            }
            else
            {
                _view.gameObject.SetActive(false);
                _buttonView.SetActiveEImage(false);
            }
        }

        private void SaveCounters()
        {
            YandexGame.savesData.SaveFishesCountData(_fishesManager.CatchedFishes);
            YandexGame.savesData.SaveResourcesCountData(_resourcesManager.ResCounters);
        }

        public void OnChestButtonClick()
        {
            _interAd.ShowAd();
            _buttonView.SetActiveEImage(false);
            _view.gameObject.SetActive(true);
            _rodView.SetOffHappyFace();

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialRod))
            {
                _tutorial.ShowHowCatchFishOnRod();
            }

            if (_playerNearbyChecker.GetPlayer() != null)
            {
               TakeFish(_playerNearbyChecker.GetPlayer().GetFish());
            }

            SaveCounters();
        }
    }
}

