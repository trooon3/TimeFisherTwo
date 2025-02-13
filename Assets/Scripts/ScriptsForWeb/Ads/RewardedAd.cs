using YG;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class RewardedAd : MonoBehaviour
    {
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
                case 1:
                    SpeedUp();
                    return;
                case 2:
                    IncreaseCountCatchedFish();
                    return;
                case 3:
                    RodSpeedUp();
                    return;
                case 4:
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

