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
    private FishType _fishFoodFor;
    private FishType _cathchingFish;
    private Resource _resourceToUpgrade;

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
                break;

            case 1:
                _countResourseToUpgrade = 25;
                break;

            case 2:
                _countResourseToUpgrade = 50;
                break;

            case 3:
                _countResourseToUpgrade = 75;
                break;

            case 4:
                _countResourseToUpgrade = 100;
                break;

            default:
                break;
        }
    }
}
