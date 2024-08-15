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
    [SerializeField] private RodCatchViewer _rodView;
    [SerializeField] private TutorialViewer _tutorial;
    [SerializeField] private DataSaver _saver;

    private List<ResourceCounter> _resources;
    private List<FishTypeCounter> _catchedFishes;
    
    private bool _isActiveIncreaseAd;
    private bool _isTutorialShowed;

    private string _fishesCountSaves = "FishTypeCount";
    private string _resurcesCountSaves = "ResourcesCount";

    private Coroutine _coroutine;
    private WaitForSeconds _increaseTime = new WaitForSeconds(60f);
    private Player _player;
    public List<SeaCreature> AllFishes => _allFishes;
    public List<FishTypeCounter> CatchedFishes => _catchedFishes;
    public DataSaver Saver => _saver;

    public UnityAction FishTransferred;
    public UnityAction ResourceCountChanged;


    private void Awake()
    {
        _catchedFishes = _saver.LoadFishesCountData(_fishesCountSaves);
        _resources = _saver.LoadResourcesCountData(_resurcesCountSaves);

        if (_catchedFishes == null)
        {
            _catchedFishes = new List<FishTypeCounter>();

            foreach (var creature in _allFishes)
            {
                FishTypeCounter counter = new FishTypeCounter(creature.FishType);
                _catchedFishes.Add(counter);
            }
        }
    }

    public void GetAllFishes()
    {
        foreach (var fish in _catchedFishes)
        {
            for (int i = 0; i < 1000; i++)
            {
                fish.Increase();
            }
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
        foreach (FishTypeCounter catchedFish in _catchedFishes)
        {
            if (catchedFish.Type == fish.Type)
            {
                catchedFish.Increase();
                FishTransferred?.Invoke();
                _spawner.SetOffFish(fish);
            }
        }
    }

    public void AddFishOnRod(FishType type)
    {
        foreach (FishTypeCounter catchedFish in _catchedFishes)
        {
            if (catchedFish.Type == type)
            {
                catchedFish.Increase();
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

        _saver.SaveFishesCountData(_fishesCountSaves, _catchedFishes);
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

        _saver.SaveResourcesCountData(_resurcesCountSaves, _resources);
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

        _saver.SaveResourcesCountData(_resurcesCountSaves, _resources);
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

        _saver.SaveFishesCountData(_fishesCountSaves, _catchedFishes);
        _saver.SaveResourcesCountData(_resurcesCountSaves, _resources);
    }
}
