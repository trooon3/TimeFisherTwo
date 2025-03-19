using YG;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class RewardedAd : MonoBehaviour
    {
        private const int SpeedUpCommand = 1;
        private const int IncreaseCountCatchedFishCommand = 2;
        private const int RodSpeedUpCommand = 3;
        private const int ResourcesIncreaseCommand = 4;

        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Bag _bag;
        [SerializeField] private Rod _rod;
        [SerializeField] private Closet _closet;

        [SerializeField] private Image _resourcesIncreaseSlider;
        [SerializeField] private Image _rodSpeedUpSlider;
        [SerializeField] private Image _speedUpSlider;
        [SerializeField] private Image _increaseCountCatchedFishSlider;

        [SerializeField] private AdTimeWorkView _adTimeWork;

        private void Start()
        {
            _rodSpeedUpSlider.fillAmount = 0;
        }

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        private void Rewarded(int id)
        {
            switch (id)
            {
                case SpeedUpCommand:
                    SpeedUp();
                    return;
                case IncreaseCountCatchedFishCommand:
                    IncreaseCountCatchedFish();
                    return;
                case RodSpeedUpCommand:
                    RodSpeedUp();
                    return;
                case ResourcesIncreaseCommand:
                    ResourcesIncrease();
                    return;
                default:
                    return;
            }
        }

        private void SpeedUp()
        {
            _adTimeWork.StartShowAdTimeWork(_speedUpSlider, _mover.IncreaseTimeSec);
            _mover.SetActiveIncrease();
        }

        private void IncreaseCountCatchedFish()
        {
            _adTimeWork.StartShowAdTimeWork(_increaseCountCatchedFishSlider, _bag.IncreaseTimeSec);
            _bag.SetActiveIncrease();
        }

        private void RodSpeedUp()
        {
            _adTimeWork.StartShowAdTimeWork(_rodSpeedUpSlider, _rod.IncreaseTimeSec);
            _rod.SetActiveIncrease();
        }

        private void ResourcesIncrease()
        {
            _adTimeWork.StartShowAdTimeWork(_resourcesIncreaseSlider, _closet.IncreaseTimeSec);
            _closet.SetActiveIncrease();
        }
    }
}

