using System.Collections.Generic;
using UnityEngine;
using YG;
using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.ScripsForWeb;
using Assets.Scripts.EquipmentScripts;
using Assets.Scripts.Increaseble;

namespace Assets.Scripts.BagScripts
{
    [RequireComponent(typeof(BagAudio))]
    [RequireComponent(typeof(FishStorage))]
    [RequireComponent(typeof(LeaderboardController))]
    [RequireComponent(typeof(BagAdBoostController))]
    public class Bag : Equipment, IIncreaseble
    {
        [SerializeField] private LeaderboardController _leaderboardController;
        [SerializeField] private BagAdBoostController _adBoostController;
        [SerializeField] private BagAudio _bagAudio;
        [SerializeField] private FishStorage _fishStorage;
        [SerializeField] private UpgradeCriterion[] _upgradeCriteria;
       
        private float _maxFishCount;

        private int _upgradeCost;
        public int Level => base.Level;
        public Resource ResourceToUpgrade => base.ResourceToUpgrade;

        private void Awake()
        {
            SetLevel(YandexGame.savesData.LoadLevel());
            _upgradeCost = UpgradeCost;
            NextLevel = (Level + 1).ToString();
            SetResourceType(Resource.SeaWeed);
            CheckLevel();
        }

        public override void CheckLevel()
        {
            SmartCheckLevel(Level, _upgradeCriteria, ref _upgradeCost, ref _maxFishCount);
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