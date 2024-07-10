using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Closet : MonoBehaviour
{
    [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
    [SerializeField] private FishSpawner _spawner;
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    [SerializeField] private ClosetView _view;
    [SerializeField] private ActiveButtonView _buttonView;
    [SerializeField] private RodCatchViewer _rodView;

    private List<ResourceCounter> _resources;
    private List<FishTypeCounter> _catchedFishes;
    private Player _player;
    public List<SeaCreature> AllFishes => _allFishes;
    public List<FishTypeCounter> CatchedFishes => _catchedFishes;
    private bool _isActiveIncreaseAd;

    private Coroutine _coroutine;
    private WaitForSeconds _increaseTime = new WaitForSeconds(60f);

    public UnityAction FishTransferred;
    public UnityAction ResourceCountChanged;

    [SerializeField] private TutorialViewer _tutorial;
    private bool _isTutorialShowed;

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

    public bool CheckCanPaySkin(SkinView skin)
    {
        SkinCost cost = skin.Cost;
        foreach (var fishPrice in cost.FishCountPrices)
        {
            foreach (var fish in _catchedFishes)
            {
                if (fishPrice.Type == fish.Type)
                {
                    if (fishPrice.Cost > fish.Count)
                    {
                        return false;
                    }
                }
            }
        }

        PaySkin(skin);
        return true;
    }

    private void PaySkin(SkinView skin)
    {
        SkinCost cost = skin.Cost;

        foreach (var fishPrice in cost.FishCountPrices)
        {
            for (int i = 0; i < fishPrice.Cost; i++)
            {
                RemoveFish(fishPrice.Type);
            }
        }
    }

    private void AddResources(Fish fish)
    {
        foreach (var item in _resources)
        {
            if (item.Resource.ToString() == fish.Resource.ToString())
            {
                if (_isActiveIncreaseAd)
                {
                    item.Increase();
                }

                item.Increase();
            }
        }
        
        ResourceCountChanged?.Invoke();
        _tutorial.ShowWhereUpgrade();
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
            OnClosetButtonClick();
        }
        else if (_playerNearbyChecker.IsPlayerNearby == false)
        {
            _view.gameObject.SetActive(false);
        }
    }

    public void OnClosetButtonClick()
    {
        _buttonView.SetActiveEImage(false);
        _view.gameObject.SetActive(true);
        _rodView.SetOffHappyFace();

        if (_isTutorialShowed == false)
        {
            _tutorial.ShowHowCatchFishOnRod();
            _isTutorialShowed = true;
        }

        if (_playerNearbyChecker.GetPlayer() != null)
        {
            _player = _playerNearbyChecker.GetPlayer();

            TakeFish(_player.GetFish());
        }
    }
}
