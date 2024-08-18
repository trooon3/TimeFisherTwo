using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Bag : MonoBehaviour , IUpgradable
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private AudioClip _catchSound;
    [SerializeField] private TutorialViewer _tutorial;
    [SerializeField] private DataSaver _saver;

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
    private WaitForSeconds _increaseTime = new WaitForSeconds(60f);
    private string _levelSave = "BagLevel";
    private string _tutorialShowedKey = "TutorialKey";

    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public int CountResourseToUpgrade => _countResourseToUpgrade;
    public int FishesInsideCount => _fishesInsideCount;
    public int Level;
    public bool IsTutorialShowed;
    public string NextLevel { get; private set; }

    public UnityAction FishCountChanged;
    public UnityAction Upgraded;
    public UnityAction BagFilled;
    public UnityAction BagDevastated;

    private void Awake()
    {
        NextLevel = (Level + 1).ToString();
        _resourceToUpgrade = Resource.SeaWeed;
        _audioSource = GetComponent<AudioSource>();
        CheckLevel();

        var dtoTutorial = _saver.LoadTutorialData(_tutorialShowedKey);
        ApplySaves(dtoTutorial);
    }

    public void SetActiveIncrease()
    {
        _isActiveIncreaseAd = true;
        StartIncreaseTimer();
    }

    private void ApplySaves(DTOTutorial dtoTutorial)
    {
        if (dtoTutorial != null)
        {
            _isTutorialShowed = dtoTutorial.IsShowed;
        }

        Level = _saver.LoadLevel(_levelSave);
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
        _fishesInsideCount = _fishes.Count;
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
            _fishesInsideCount = _fishes.Count;
            FishCountChanged?.Invoke();
            _audioSource.PlayOneShot(_catchSound);
            _countAllCatchedFishes++;

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

        _saver.SaveLevel(_levelSave, Level);
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
