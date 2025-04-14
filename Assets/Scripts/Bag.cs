using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.Events;
using System.Collections;
using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.ScripsForWeb.Leaderboard;
using Assets.Scripts.Tutorial;
using Assets.Scripts.Saves;
using Assets.Scripts.UI;

namespace Assets.Scripts
{
    public class Bag : Equipment
    {
        [SerializeField] private YandexLeaderboard _yandexLeaderboard;
        [SerializeField] private LeaderboardYG _leaderboardYG;
        [SerializeField] private AudioClip _catchSound;
        [SerializeField] private TutorialViewer _tutorial;
        [SerializeField] private ButtonChangerController _buttonChangerController;
        [SerializeField] private UpgradeCriterion[] upgradeCriteria;

        private readonly float _increaseTimeSec = 60f;
       
        private float _maxFishCount;
        private int _countAllCatchedFishes;
        private int _fishesInsideCount;

        private bool _isActiveIncreaseAd;

        private readonly List<Fish> _fishes = new List<Fish>();
        private AudioSource _audioSource;
        private Coroutine _coroutine;
        private WaitForSeconds _increaseTime;

        public int CountResourseToUpgrade => _upgradeCost;
        public int FishesInsideCount => _fishesInsideCount;
        public int Level => _level;
        public float IncreaseTimeSec => _increaseTimeSec;
        public bool IsActiveIncreaseAd => _isActiveIncreaseAd;

        public event UnityAction FishCountChanged;
        public event UnityAction BagFilled;
        public event UnityAction BagDevastated;

        private void Awake()
        {
            _level = YandexGame.savesData.LoadLevel();
            NextLevel = (_level + 1).ToString();
            _resourceToUpgrade = Resource.SeaWeed;
            _increaseTime = new WaitForSeconds(_increaseTimeSec);
            _audioSource = GetComponent<AudioSource>();
            CheckLevel();
        }

        public override void CheckLevel()
        {
            SmartCheckLevel(_level, upgradeCriteria, ref _upgradeCost, ref _maxFishCount);
        }

        public void SetActiveIncrease()
        {
            _isActiveIncreaseAd = true;
            _buttonChangerController.SetButtonChangerOff();
            StartIncreaseTimer();
        }

        public List<Fish> GetFish()
        {
            List<Fish> fishes = new List<Fish>();

            foreach (var fish in _fishes)
            {
                if (_isActiveIncreaseAd)
                {
                    fishes.Add(fish);
                }

                fishes.Add(fish);
            }

            _fishes.Clear();
            _fishesInsideCount = _fishes.Count;
            FishCountChanged?.Invoke();
            BagDevastated?.Invoke();

            return fishes;
        }

        public void SetScore()
        {
            if (YandexGame.auth)
            {
                YandexGame.NewLeaderboardScores(_leaderboardYG.nameLB, _countAllCatchedFishes);
            }
        }

        public bool TryAddFish(Fish fish)
        {
            if (_fishes.Count < _maxFishCount)
            {
                _fishes.Add(fish);
                _fishesInsideCount = _fishes.Count;
                FishCountChanged?.Invoke();
                _audioSource.PlayOneShot(_catchSound);
                _countAllCatchedFishes++;
                _leaderboardYG.NewScore(_countAllCatchedFishes);

                if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
                {
                    _tutorial.ShowWhereFishesCount();
                }

                if (_fishes.Count == _maxFishCount)
                {
                    BagFilled?.Invoke();
                }

                _tutorial.SetOffControlTutorial();

                return true;
            }

            fish.ShowFillBag(true);

            return false;
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
        }
    }
}