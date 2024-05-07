using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Closet : MonoBehaviour
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    [SerializeField] private FishSpawner _spawner;
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    private List<ResourceCounter> _resources;
    private List<FishTypeCounter> _catchedFishes;
    private Player _player;
    public List<SeaCreature> AllFishes => _allFishes;
    public List<FishTypeCounter> CatchedFishes => _catchedFishes;
    public UnityAction FishTransferred;

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

    private void AddResources(Fish fish)
    {
        foreach (var item in _resources)
        {
            if (item.Resource.ToString() == fish.Resource.ToString())
            {
                item.Increase();
            }
        }
    }

    private void Update()
    {
        if (_playerNearbyChecker.IsPlayerNearby)
        {
            if (_playerNearbyChecker.GetPlayer() != null)
            {
                _player = _playerNearbyChecker.GetPlayer();

                TakeFish(_player.GetFish());
            }
        }
    }

}
