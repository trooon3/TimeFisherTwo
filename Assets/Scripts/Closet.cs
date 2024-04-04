using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    private List<ResourceCounter> _resources;
    private List<FishTypeCounter> _catchedFishes;

    private Player _player;
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;

    private void Awake()
    {
        _resources = new List<ResourceCounter> { new ResourceCounter(Resource.FishBones), new ResourceCounter(Resource.SeaWeed) };
        _catchedFishes = new List<FishTypeCounter>();

        foreach (var creature in _allFishes)
        {
            _catchedFishes.Add(new FishTypeCounter(creature.FishType));
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
            Debug.Log("����� � �����");
            if (item.Type == fish.Type)
            {
                Debug.Log("��������� �������� �������� ����");
                item.Increase();
            }
        }
    }

    private void AddResources(Fish fish)
    {
        foreach (var item in _resources)
        {
            if (item.Resource == fish.Resource)
            {
                item.Increase();
                Debug.Log("��������� �������� �������");
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
