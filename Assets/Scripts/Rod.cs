using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rod : MonoBehaviour , IUpgradable
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;

    private int _spicesFishRewardCount = 2;
    private int _maxLevel = 5;
    private int _countResourseToUpgrade;
    public int Level { get; private set; } = 1;
    private Resource _resourceToUpgrade;
    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public UnityAction Upgraded;

    private void Start()
    {
        _resourceToUpgrade = Resource.FishBones;

        CheckLevel();
    }

    public void Upgrade()
    {
        if (Level < _maxLevel)
        {
            Level++;
            Upgraded?.Invoke();
        }
        CheckLevel();
    }

    public FishType GetFishFoodFor(FishType type)
    {
        foreach (var fish in _allFishes)
        {
            if (fish.FishType == type)
            {
                return fish.FoodFor.FishType;
            }
        }

        return 0;
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
            case 1:
                _countResourseToUpgrade = 10;
                break;

            case 2:
                _countResourseToUpgrade = 25;
                break;

            case 3:
                _countResourseToUpgrade = 50;
                break;

            case 4:
                _countResourseToUpgrade = 75;
                break;

            case 5:
                _countResourseToUpgrade = 100;
                break;

            default:
                break;
        }
    }
}
