using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Closet : MonoBehaviour
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    [SerializeField] private FishSpawner _spawner;
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    [SerializeField] private ClosetView _view;
    [SerializeField] private ActiveButtonView _buttonView;

    private List<ResourceCounter> _resources;
    private List<FishTypeCounter> _catchedFishes;
    private Player _player;
    public List<SeaCreature> AllFishes => _allFishes;
    public List<FishTypeCounter> CatchedFishes => _catchedFishes;

    public UnityAction FishTransferred;
    public UnityAction ResourceCountChanged;

    private void Awake()
    {
        _resources = new List<ResourceCounter> { new ResourceCounter(Resource.FishBones), new ResourceCounter(Resource.SeaWeed)};
        _catchedFishes = new List<FishTypeCounter>();

        foreach (var creature in _allFishes)
        {
            FishTypeCounter counter = new FishTypeCounter(creature.FishType);
            _catchedFishes.Add(counter);
        }
    }

    public void TakeFish(List<Fish> fishes)
    {
        for (int i = 0; i < fishes.Count; i++)
        {
            AddFish(fishes[i]);
            AddResources(fishes[i]);
        }
    }

    private void AddFish(Fish fish)
    {
        foreach (FishTypeCounter item in _catchedFishes)
        {
            if (item.Type == fish.Type)
            {
                item.Increase();
                FishTransferred?.Invoke();
                _spawner.SetOffFish(fish);
            }
        }
    }

    public void AddFishOnRod(FishType type)
    {
        foreach (FishTypeCounter item in _catchedFishes)
        {
            if (item.Type == type)
            {
                item.Increase();
                FishTransferred?.Invoke();
            }
        }
    }

    public void RemoveFish(FishType fish)
    {
        foreach (FishTypeCounter item in _catchedFishes)
        {
            if (item.Type == fish)
            {
                item.Decrease();
                FishTransferred?.Invoke();
            }
        }
    }

    private void AddResources(Fish fish)
    {
        foreach (var item in _resources)
        {
            if (item.Resource.ToString() == fish.Resource.ToString())
            {
                item.Increase();
            }
        }
        
        ResourceCountChanged?.Invoke();
    }

    public void SpendResources(int count, Resource type)
    {
        foreach (var resourceType in _resources)
        {
            if (resourceType.Resource == type)
            {
                for (int i = 0; i < count; i++)
                {
                    resourceType.Decrease();
                }
            }
        }

        ResourceCountChanged?.Invoke();
    }

    public bool CheckIsCanPay(Resource resource, int count)
    {
        foreach (var resourceType in _resources)
        {
            if (resourceType.Resource == resource)
            {
                if (resourceType.Count >= count)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetFishBonesCount()
    {
        foreach (var resource in _resources)
        {
            if (resource.Resource == Resource.FishBones)
            {
                return resource.Count;
            }
        }
        return 0;
    }

    public int GetSeaWeedCount()
    {
        foreach (var resource in _resources)
        {
            if (resource.Resource == Resource.SeaWeed)
            {
                return resource.Count;
            }
        }
        return 0;
    }

    private void Update()
    {
        if (_playerNearbyChecker.IsPlayerNearby)
        {
            _buttonView.SetActiveEImage(true);
        }
        else
        {
            _buttonView.SetActiveEImage(false);
        }

        if (_playerNearbyChecker.IsPlayerNearby && Input.GetKey(KeyCode.E))
        {
            _buttonView.SetActiveEImage(false);
            _view.gameObject.SetActive(true);

            if (_playerNearbyChecker.GetPlayer() != null)
            {
                _player = _playerNearbyChecker.GetPlayer();

                TakeFish(_player.GetFish());
            }
        }
        else if (_playerNearbyChecker.IsPlayerNearby == false)
        {
            _view.gameObject.SetActive(false);
        }
    }

}
