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
    public class Rod : MonoBehaviour, IUpgradable
    {
        private const int ZeroLevelCommand = 0;
        private const int FirstLevelCommand = 1;
        private const int SecondLevelCommand = 2;
        private const int ThirdLevelCommand = 3;
        private const int FourthLevelCommand = 4;

        [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private RodCatchViewer _catchViewer;
        [SerializeField] private ClosetView _closetView;
        [SerializeField] private DataSaver _saver;
        [SerializeField] private ButtonChangerController _buttonChangerController;

        private int _countResourseToUpgrade;
        private int _level;
        private readonly int _maxLevel = 5;
        private float _catchingSpeed;
        private readonly string _levelDataKey = "RodKey";

        private FishType _fishFoodFor;
        private FishType _cathchingFish;
        private Resource _resourceToUpgrade;

        private Coroutine _coroutine;
        private WaitForSeconds _increaseTime;
        private bool _isActiveIncreaseAd;
        private readonly float _increaseTimeSec = 60f;

        public bool IsActiveIncreaseAd => _isActiveIncreaseAd;
        public string NextLevel { get; private set; }
        public Resource ResourceToUpgrade => _resourceToUpgrade;
        public int CountResourseToUpgrade => _countResourseToUpgrade;
        public float CatchingSpeed => _catchingSpeed;
        public FishType FishFoodFor => _fishFoodFor;
        public int Level => _level;
        public float IncreaseTimeSec => _increaseTimeSec;

        public UnityAction Upgraded;

        private void Awake()
        {
            _increaseTime = new WaitForSeconds(_increaseTimeSec);
            NextLevel = (_level + 1).ToString();
            _resourceToUpgrade = Resource.FishBones;
            CheckLevel();

            var dtoLevel = _saver.LoadLevelData(_levelDataKey);
            ApplySaves(dtoLevel);
        }

        public void SetActiveIncrease()
        {
            _catchingSpeed = _catchingSpeed * 2;
            _isActiveIncreaseAd = true;
            _buttonChangerController.SetButtonChangerOff();
            StartIncreaseTimer();
        }

        public void Upgrade()
        {
            if (_level < _maxLevel)
            {
                _level++;

                if (_level + 1 > _maxLevel)
                {
                    NextLevel = "MAX";
                }
                else
                {
                    NextLevel = (_level + 1).ToString();
                }

                Upgraded?.Invoke();
            }

            CheckLevel();
            DTOLevel dTOLevel = new DTOLevel();
            dTOLevel.Init(_countResourseToUpgrade, _level);
            _saver.SaveLevelData(_levelDataKey, dTOLevel);
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

        public Resource GetResourceToUpgrade()
        {
            return ResourceToUpgrade;
        }

        public int GetResourceCountToUpgrade()
        {
            return _countResourseToUpgrade;
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
                _countResourseToUpgrade = dtoLevel.Count;
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
            CheckLevel();
        }

        private void CheckLevel()
        {
            switch (_level)
            {
                case ZeroLevelCommand:
                    _countResourseToUpgrade = 10;
                    _catchingSpeed = 0.01f;
                    break;

                case FirstLevelCommand:
                    _countResourseToUpgrade = 25;
                    _catchingSpeed = 0.02f;
                    break;

                case SecondLevelCommand:
                    _countResourseToUpgrade = 50;
                    _catchingSpeed = 0.04f;
                    break;

                case ThirdLevelCommand:
                    _countResourseToUpgrade = 75;
                    _catchingSpeed = 0.1f;
                    break;

                case FourthLevelCommand:
                    _countResourseToUpgrade = 100;
                    _catchingSpeed = 0.2f;
                    break;

                default:
                    break;
            }
        }
    }
}

