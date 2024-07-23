using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Bag : MonoBehaviour , IUpgradable
{
    private int _maxLevel = 5;
    private int _maxFishCount;
    private int _countResourseToUpgrade;
    private int _countAllCatchedFishes;
    private bool _isActiveIncreaseAd;

    private List<Fish> _fishes = new List<Fish>();
    private Resource _resourceToUpgrade;
    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private WaitForSeconds _increaseTime = new WaitForSeconds(60f);

    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private AudioClip _catchSound;
    [SerializeField] private TutorialViewer _tutorial;
    private bool _isTutorialShowed;

    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public int CountResourseToUpgrade => _countResourseToUpgrade;
    public int FishesInsideCount;
    public int Level { get; private set; }
    public string NextLevel { get; private set; }

    public UnityAction FishCountChanged;
    public UnityAction Upgraded;
    public UnityAction BagFilled;
    public UnityAction BagDevastated;

    private void Awake()
    {
        Level = 0;
        NextLevel = (Level + 1).ToString();
        _resourceToUpgrade = Resource.SeaWeed;
        _audioSource = GetComponent<AudioSource>();
        CheckLevel();
    }

    public void SetActiveIncrease()
    {
        _isActiveIncreaseAd = true;
        StartIncreaseTimer();
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
        FishesInsideCount = _fishes.Count;
        FishCountChanged?.Invoke();
        BagDevastated?.Invoke();

        return fishes;
    }

    public void SetScore()
    {
        _yandexLeaderboard.SetPlayerScore(_countAllCatchedFishes);
    }

    public bool TryAddFish(Fish fish)
    {
        if (_fishes.Count < _maxFishCount)
        {
            _fishes.Add(fish);
            FishesInsideCount = _fishes.Count;
            FishCountChanged?.Invoke();
            _audioSource.PlayOneShot(_catchSound);
            _countAllCatchedFishes++;

            if (_isTutorialShowed == false)
            {
                _tutorial.ShowWhereFishesCount();
                _isTutorialShowed = true;
            }

            if (_fishes.Count == _maxFishCount)
            {
                BagFilled?.Invoke();
            }

            _tutorial.SetOffControlTutorial();

            return true;
        }

        return false;
    }


    public void Upgrade()
    {
        if (Level < _maxLevel)
        {
            Level++;

            if (Level + 1 > _maxLevel)
            {
                NextLevel = "MAX";
            }
            else
            {
                NextLevel = (Level + 1).ToString();
            }

            Upgraded?.Invoke();
        }
        CheckLevel();
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
        switch (Level)
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
