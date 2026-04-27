using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Fishes;
using Assets.Scripts.Increaseble;
using Assets.Scripts.UI;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Assets.Scripts.FishResources
{
    [JsonObject(MemberSerialization.Fields)]
    public class ResourcesManager : MonoBehaviour, IIncreaseble
    {
        [SerializeField] private List<ResourceCounter> _resources;
        private ButtonChangerController _buttonChangerController;

        private Coroutine _coroutine;
        private WaitForSeconds _increaseTime;
        private bool _isActiveIncreaseAd;
        private float _increaseTimeSec = 60f;

        public List<ResourceCounter> ResCounters => _resources;
        public bool IsActiveIncreaseAd => _isActiveIncreaseAd;
        public float IncreaseTimeSec => _increaseTimeSec;

        public event UnityAction ResourceCountChanged;

        private void Start()
        {
            _resources = YandexGame.savesData.LoadResourcesCountData();
            _increaseTime = new WaitForSeconds(_increaseTimeSec);
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadCounter;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadCounter;
        }

        private void LoadCounter()
        {
            _resources = YandexGame.savesData.LoadResourcesCountData();
        }

        private void StartIncreaseTimer()
        {
            if (_coroutine != null)
            {
                StopCoroutine(IncreaseTimer());
            }

            _coroutine = StartCoroutine(IncreaseTimer());
        }

        private int GetResourceCount(Resource resourceType)
        {
            foreach (var resource in _resources)
            {
                if (resource.Resource == resourceType)
                {
                    return resource.Count;
                }
            }

            return 0;
        }

        private IEnumerator IncreaseTimer()
        {
            yield return _increaseTime;
            _isActiveIncreaseAd = false;
            _buttonChangerController.SetButtonChangerOn();
        }

        public int GetFishBonesCount()
        {
            return GetResourceCount(Resource.FishBones);
        }

        public int GetSeaWeedCount()
        {
            return GetResourceCount(Resource.SeaWeed);
        }

        public void AddResources(Fish fish)
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

            YandexGame.savesData.SaveResourcesCountData(_resources);
            ResourceCountChanged?.Invoke();
        }

        public void SetActiveIncrease()
        {
            _isActiveIncreaseAd = true;
            _buttonChangerController.SetButtonChangerOff();
            StartIncreaseTimer();
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

            YandexGame.savesData.SaveResourcesCountData(_resources);
            ResourceCountChanged?.Invoke();
        }
    }
}