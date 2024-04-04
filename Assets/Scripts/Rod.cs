using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour , IUpgradable
{
    private FishGetter _getter;
    private int _spicesFishRewardCount = 2;
    private int _maxLevel = 5;
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    private int _countResourseToUpgrade;
    public int Level { get; private set; } = 1;
    private Resource _resourceToUpgrade;
    public Resource ResourceToUpgrade => _resourceToUpgrade;

    private void Start()
    {
        _resourceToUpgrade = Resource.FishBones;

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

    private void Update()
    {
        //if (_playerNearbyChecker.IsPlayerNearby && Input.GetKeyDown(KeyCode.E))
        //{
        //    // открыть UI меню и там уже вызвать методы ниже.
        //}
    }

    public void Upgrade()
    {
        if (Level < _maxLevel)
        {
            Level++;
        }
    }

    public Fish GetFish(SeaCreature fish)
    {

        //if (если рак в специях)
        //{
        //  GetFishes(fish);
        //}

        return _getter.GetFishOnHook(fish);
    }

    private List<Fish> GetFishes(SeaCreature fish)
    {
        List<Fish> fishes = new List<Fish>();

        for (int i = 0; i < 4; i++)
        {
            fishes.Add(_getter.GetFishOnHook(fish.FoodFor));
        }

        return fishes;
    }

    public Resource GetResourceToUpgrade()
    {
        return ResourceToUpgrade;
    }

    public int GetResourceCountToUpgrade()
    {
        return _countResourseToUpgrade;
    }
}
