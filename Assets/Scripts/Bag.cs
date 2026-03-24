using System.Collections.Generic;
using UnityEngine;
using YG;
using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BagAudio))]
    [RequireComponent(typeof(FishStorage))]
    [RequireComponent(typeof(LeaderboardController))]
    [RequireComponent(typeof(BagAdBoostController))]
    public class Bag : Equipment, IInreaseble
    {
        [SerializeField] private LeaderboardController _leaderboardController;
        [SerializeField] private BagAdBoostController _adBoostController;
        [SerializeField] private BagAudio _bagAudio;
        [SerializeField] private FishStorage _fishStorage;
        [SerializeField] private UpgradeCriterion[] upgradeCriteria;
       
        private float _maxFishCount;
        private int _fishesInsideCount;

        public int CountResourseToUpgrade => _upgradeCost;
        public int FishesInsideCount => _fishesInsideCount;
        public int Level => _level;

        private void Awake()
        {
            _level = YandexGame.savesData.LoadLevel();
            NextLevel = (_level + 1).ToString();
            _resourceToUpgrade = Resource.SeaWeed;
            CheckLevel();
        }

        public override void CheckLevel()
        {
            SmartCheckLevel(_level, upgradeCriteria, ref _upgradeCost, ref _maxFishCount);
        }

        public void SetActiveIncrease()
        {
            _adBoostController.ActivateBoost();
        }

        public List<Fish> GetFish()
        {
            return _fishStorage.GetFish();
        }

        public bool TryAddFish(Fish fish)
        {
            if (_fishStorage.TryAddFish(fish))
            {
                _bagAudio.PlayCatchSound();
                _leaderboardController.AddScore();
                return true;
            }
            else
            {
                fish.CatchTimer.ShowFillBag(true);
                return false;
            }
        }
    }
}