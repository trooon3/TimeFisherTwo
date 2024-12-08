using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.Events;
using System.Collections;

public class Bag : MonoBehaviour , IUpgradable
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private AudioClip _catchSound;
    [SerializeField] private TutorialViewer _tutorial;
    [SerializeField] private DataSaver _saver;
    [SerializeField] private ButtonChangerController _buttonChangerController;
    [SerializeField] private LeaderboardYG _leaderboardYG;

    private int _level;
    private float _increaseTimeSec = 60f;
    private int _maxLevel = 5;
    private int _maxFishCount;
    private int _countResourseToUpgrade;
    private int _countAllCatchedFishes;
    private int _fishesInsideCount;

    private bool _isActiveIncreaseAd;
    private bool _isTutorialShowed;

    private List<Fish> _fishes = new List<Fish>();
    private Resource _resourceToUpgrade;
    private AudioSource _audioSource;
    private Coroutine _coroutine; 
    private WaitForSeconds _increaseTime;
    private string _levelDataKey = "BagKey";
    private string _tutorialShowedKey = "TutorialKey";

    public string NextLevel { get; private set; }
    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public int CountResourseToUpgrade => _countResourseToUpgrade;
    public int FishesInsideCount => _fishesInsideCount;
    public int Level => _level;
    public float IncreaseTimeSec => _increaseTimeSec;
    public bool IsActiveIncreaseAd => _isActiveIncreaseAd;

    public bool IsTutorialShowed;
    public UnityAction FishCountChanged;
    public UnityAction Upgraded;
    public UnityAction BagFilled;
    public UnityAction BagDevastated;

    private void Awake()
    {
        NextLevel = (_level + 1).ToString();
        _resourceToUpgrade = Resource.SeaWeed;
        _increaseTime = new WaitForSeconds(_increaseTimeSec);
        _audioSource = GetComponent<AudioSource>();
        CheckLevel();

        var dtoTutorial = _saver.LoadTutorialData(_tutorialShowedKey);
        var dtoLevel = _saver.LoadLevelData(_levelDataKey);
        ApplySaves(dtoTutorial, dtoLevel);
    }

    public void SetActiveIncrease()
    {
        _isActiveIncreaseAd = true;
        _buttonChangerController.SetButtonChangerOff();
        StartIncreaseTimer();
    }

    private void ApplySaves(DTOTutorial dtoTutorial, DTOLevel dtoLevel)
    {
        if (dtoTutorial != null)
        {
            _isTutorialShowed = dtoTutorial.IsShowed;
        }

        if (dtoLevel != null)
        {
            _level = dtoLevel.Level;
            _countResourseToUpgrade = dtoLevel.Count;
            _countAllCatchedFishes = dtoLevel.Score;
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
           //SetScore();

            if (_isTutorialShowed == false)
            {
                _tutorial.ShowWhereFishesCount();
                _isTutorialShowed = true;
                _saver.SaveTutorialData(_tutorialShowedKey, new DTOTutorial { IsShowed = _isTutorialShowed });
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
        _saver.SaveLevelData(_levelDataKey, new DTOLevel { Count = _countResourseToUpgrade, Level = _level });
    }

    public Resource GetResourceToUpgrade()
    {
        return ResourceToUpgrade;
    }

    public int GetResourceCountToUpgrade()
    {
        return _countResourseToUpgrade;
    }

    private void CheckLevel()
    {
        switch (_level)
        {
            case 0:
                _maxFishCount = 4;
                _countResourseToUpgrade = 10;
                break;

            case 1:
                _maxFishCount = 6;
                _countResourseToUpgrade = 25;
                break;

            case 2:
                _maxFishCount = 8;
                _countResourseToUpgrade = 50;
                break;

            case 3:
                _maxFishCount = 10;
                _countResourseToUpgrade = 75;
                break;

            case 4:
                _maxFishCount = 12;
                _countResourseToUpgrade = 100;
                break;

            default:
                break;
        }
    }
}
