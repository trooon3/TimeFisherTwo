using System.Collections.Generic;
using Assets.Scripts.FishResources;
using Assets.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Assets.Scripts.BagScripts;
using Assets.Scripts.RodScripts;
using Assets.Scripts.Increaseble;
using Assets.Scripts.RewardTypes;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class RewardedAd : MonoBehaviour
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Bag _bag;
        [SerializeField] private BagAdBoostController _adBoostController;
        [SerializeField] private Rod _rod;
        [SerializeField] private ResourcesManager _resourcesManager;

        [SerializeField] private Image _resourcesIncreaseSlider;
        [SerializeField] private Image _rodSpeedUpSlider;
        [SerializeField] private Image _speedUpSlider;
        [SerializeField] private Image _increaseCountCatchedFishSlider;

        [SerializeField] private AdTimeWorkView _adTimeWork;
        private Dictionary<int, (Image slider, float increaseTime, IIncreaseble target)> _rewardCommands = new();
        
        private void Start()
        {
            _rewardCommands = new()
            {
            { ((int) RewardType.SpeedUp), (_speedUpSlider, _mover.IncreaseTimeSec, _mover) },
            { ((int) RewardType.IncreaseCountCatchedFish), (_increaseCountCatchedFishSlider, _adBoostController.BoostDuration, _bag) },
            { ((int) RewardType.RodSpeedUp), (_rodSpeedUpSlider, _rod.IncreaseTimeSec, _rod) },
            { ((int) RewardType.ResourcesIncrease), (_resourcesIncreaseSlider, _resourcesManager.IncreaseTimeSec, _resourcesManager) }
            };
            _rodSpeedUpSlider.fillAmount = 0;
        }

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        private void Rewarded(int id)
        {
            if (_rewardCommands.TryGetValue(id, out var command))
            {
                SetActiveIncrease(command.slider, command.increaseTime, command.target);
            }
        }

        private void SetActiveIncrease(Image slider, float increaseTime , IIncreaseble inreaseble)
        {
            _adTimeWork.StartShowAdTimeWork(slider, increaseTime);
            inreaseble.SetActiveIncrease();
        }
    }
}