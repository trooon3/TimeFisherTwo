using Assets.Scripts.Fishes;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.FishResources;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Assets.Scripts.Increaseble;
using Assets.Scripts.EquipmentScripts;

namespace Assets.Scripts.RodScripts
{
    public class Rod : Equipment, IIncreaseble
    {
        private const int _levelIncrement = 1;
        private readonly float _increaseTimeSec = 60f;

        [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private RodCatchViewer _catchViewer;
        [SerializeField] private ClosetView _closetView;
        [SerializeField] private ButtonChangerController _buttonChangerController;
        [SerializeField] private UpgradeCriterion[] _upgradeCriteria;

        private float _catchingSpeed;
        private int _increaseMultiplier = 2;

        private FishType _fishFoodFor;
        private FishType _cathchingFish;

        private Coroutine _coroutine;
        private WaitForSeconds _increaseTime;
        private bool _isActiveIncreaseAd;
        private int _upgradeCost;
        public bool IsActiveIncreaseAd => _isActiveIncreaseAd;
        public int CountResourseToUpgrade => _upgradeCost;
        public float CatchingSpeed => _catchingSpeed;
        public FishType FishFoodFor => _fishFoodFor;
        public float IncreaseTimeSec => _increaseTimeSec;

        public int Level => base.Level;
        public Resource ResourceToUpgrade => base.ResourceToUpgrade;

        private void Awake()
        {
            _upgradeCost = UpgradeCost; 
            SetLevel(YandexGame.savesData.LoadLevel());
            _increaseTime = new WaitForSeconds(_increaseTimeSec);
            NextLevel = (Level + _levelIncrement).ToString();
            SetResourceType(Resource.SeaWeed);
            CheckLevel();
        }

        private IEnumerator IncreaseTimer()
        {
            yield return _increaseTime;
            _isActiveIncreaseAd = false;
            _buttonChangerController.SetButtonChangerOn();
            SmartCheckLevel(Level, _upgradeCriteria, ref _upgradeCost, ref _catchingSpeed);
        }

        private void StartIncreaseTimer()
        {
            if (_coroutine != null)
            {
                StopCoroutine(IncreaseTimer());
            }

            _coroutine = StartCoroutine(IncreaseTimer());
        }

        public override void CheckLevel()
        {
            SmartCheckLevel(Level, _upgradeCriteria, ref _upgradeCost, ref _catchingSpeed);
        }

        public void SetActiveIncrease()
        {
            _catchingSpeed = _catchingSpeed * _increaseMultiplier;
            _isActiveIncreaseAd = true;
            _buttonChangerController.SetButtonChangerOff();
            StartIncreaseTimer();
        }

        public void GetFishFoodFor()
        {
            _catchViewer.StopDisplayCatchingTime();

            foreach (var fish in _allFishes)
            {
                if (fish.FishType == _cathchingFish)
                {
                    _fishFoodFor = fish.FoodFor.FishType;
                    _closetView.AddFishAndRefresh();
                }
            }
        }

        public void GetReadyCatch(FishType type)
        {
            _cathchingFish = type;

            _catchViewer.StartDisplayCatchingTime();
        }
    }
}