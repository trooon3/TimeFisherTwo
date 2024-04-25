using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour , IUpgradable
{
    private List<Fish> _fishes = new List<Fish>();
    private int _maxFishCount;
    private int _maxLevel = 5;
    private List<ResourceCounter> _resources;
    private Resource _resourceToUpgrade;
    public Resource ResourceToUpgrade => _resourceToUpgrade;
    public int Level { get; private set; } = 1;
    private int _countResourseToUpgrade;

    private void Start()
    {
        _resources = new List<ResourceCounter>();
        _resourceToUpgrade = Resource.SeaWeed;

        switch (Level)
        {
            case 1 :
                _maxFishCount = 4;
                _countResourseToUpgrade = 10;
                break;

            case 2 :
                _maxFishCount = 6;
                _countResourseToUpgrade = 25;
                break;

            case 3 :
                _maxFishCount = 8;
                _countResourseToUpgrade = 50;
                break;

            case 4 :
                _maxFishCount = 10;
                _countResourseToUpgrade = 75;
                break;

            case 5 :
                _maxFishCount = 12;
                _countResourseToUpgrade = 100;
                break;

            default:
                break;
        }
    }

    public bool CheckIsCanPay(Resource type, int count)
    {
        if (count >= GetNeedResourceCount(type))
        {
            return true;
        }

        return false;
    }

    public int GetNeedResourceCount(Resource resource)
    {
        return _resources.Count(res => _resources.GetType() == resource.GetType());
    }

    public List<Fish> GetFish()
    {
        List<Fish> fishes = new List<Fish>();

        foreach (var fish in _fishes)
        {
            fishes.Add(fish);
        }
        _fishes.Clear();

        return fishes;
    }

    public bool TryAddFish(Fish fish)
    {
        if (_fishes.Count < _maxFishCount)
        {
            _fishes.Add(fish);
            return true;
        }
        else
        {
            Debug.Log("–юкзак полон");
        }
        return false;
    }

    public Resource GetResourceToUpgrade()
    {
        return ResourceToUpgrade;
    }

    public void Upgrade()
    {
        if (Level < _maxLevel)
        {
            Level++;
        }
    }

    public int GetResourceCountToUpgrade()
    {
        return _countResourseToUpgrade;
    }

    public void SpendResources(int count, Resource type)
    {
        for (int i = 0; i < count; i++)
        {
            if (_resources[i].GetType() == type.GetType())
            {
                _resources.Remove(_resources[i]);
            }
        }
    }
}
