using Assets.Scripts.Fishes;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves;
using Assets.Scripts.Saves.DTO;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Rod : Equipment
    {
        [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private RodCatchViewer _catchViewer;
        [SerializeField] private ClosetView _closetView;
        [SerializeField] private ButtonChangerController _buttonChangerController;

        private float _catchingSpeed;

        private FishType _fishFoodFor;
        private FishType _cathchingFish;

        private Coroutine _coroutine;
        private WaitForSeconds _increaseTime;
        private bool _isActiveIncreaseAd;
        private readonly float _increaseTimeSec = 60f;
        private readonly string _levelDataKey = "RodKey";

        public bool IsActiveIncreaseAd => _isActiveIncreaseAd;
        public int CountResourseToUpgrade => _upgradeCost;
        public string LevelDataKey => _levelDataKey;
        public float CatchingSpeed => _catchingSpeed;
        public FishType FishFoodFor => _fishFoodFor;
        public int Level => _level;
        public float IncreaseTimeSec => _increaseTimeSec;

        private void Awake()
        {
           // _saver = new DataSaver();
            _increaseTime = new WaitForSeconds(_increaseTimeSec);
            NextLevel = (_level + 1).ToString();
            _resourceToUpgrade = Resource.FishBones;
            CheckLevel(ref _upgradeCost, ref _catchingSpeed);

           // var dtoLevel = _saver.LoadLevelData(_levelDataKey);
           // ApplySaves(dtoLevel);
        }

        public void SetActiveIncrease()
        {
            _catchingSpeed = _catchingSpeed * 2;
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

        private void ApplySaves(DTOLevel dtoLevel)
        {
            if (dtoLevel != null)
            {
                _level = dtoLevel.Level;
                _upgradeCost = dtoLevel.Count;
            }
        }

        private void StartIncreaseTimer()
        {
            if (_coroutine != null)
            {
                StopCoroutine(IncreaseTimer());
            }

            _coroutine = StartCoroutine(IncreaseTimer());
        }

        private IEnumerator IncreaseTimer()
        {
            yield return _increaseTime;
            _isActiveIncreaseAd = false;
            _buttonChangerController.SetButtonChangerOn();
            CheckLevel(ref _upgradeCost, ref _catchingSpeed);
        }

        private void CheckLevel(ref int upgradeCost, ref float upgradeParametr)
        {
            switch (_level)
            {
                case ZeroLevelCommand:
                    upgradeCost = 10;
                    upgradeParametr = 0.01f;
                    break;

                case FirstLevelCommand:
                    upgradeCost = 25;
                    upgradeParametr = 0.02f;
                    break;

                case SecondLevelCommand:
                    upgradeCost = 50;
                    upgradeParametr = 0.04f;
                    break;

                case ThirdLevelCommand:
                    upgradeCost = 75;
                    upgradeParametr = 0.1f;
                    break;

                case FourthLevelCommand:
                    upgradeCost = 100;
                    upgradeParametr = 0.2f;
                    break;

                default:
                    break;
            }
        }
    }
}

