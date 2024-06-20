using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rod : MonoBehaviour , IUpgradable
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    [SerializeField] private RodCatchViewer _catchViewer;
    [SerializeField] private ClosetView _closetView;
   
    private int _countResourseToUpgrade;
    private int _spicesFishRewardCount = 2;
    private int _maxLevel = 5;
    private float _catchingSpeed;

    private FishType _fishFoodFor;
    private FishType _cathchingFish;
    private Resource _resourceToUpgrade;

    private Coroutine _coroutine;
    private WaitForSeconds _increaseTime = new WaitForSeconds(60f);

    public float CatchingSpeed => _catchingSpeed;
    public int CountResourseToUpgrade => _countResourseToUpgrade;
    public int Level { get; private set; }
    public string NextLevel { get; private set; }
    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public FishType FishFoodFor => _fishFoodFor;
    public UnityAction Upgraded;

    private void Awake()
    {
        Level = 0;
        NextLevel = (Level + 1).ToString();
        _resourceToUpgrade = Resource.FishBones;

        CheckLevel();
    }

    public void SetActiveIncrease()
    {
        _catchingSpeed = _catchingSpeed * 2;
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

        CheckLevel();
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

    private void CheckLevel()
    {
        switch (Level)
        {
            case 0:
                _countResourseToUpgrade = 10;
                _catchingSpeed = 0.01f;
                break;

            case 1:
                _countResourseToUpgrade = 25;
                _catchingSpeed = 0.02f;
                break;

            case 2:
                _countResourseToUpgrade = 50;
                _catchingSpeed = 0.04f;
                break;

            case 3:
                _countResourseToUpgrade = 75;
                _catchingSpeed = 0.1f;
                break;

            case 4:
                _countResourseToUpgrade = 100;
                _catchingSpeed = 0.2f;
                break;

            default:
                break;
        }
    }
}
