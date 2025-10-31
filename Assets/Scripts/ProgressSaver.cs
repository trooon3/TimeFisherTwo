using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class ProgressSaver : MonoBehaviour
    {
        [SerializeField] private FishesManager _fishesManager;
        [SerializeField] private ResourcesManager _resourcesManager;

        private void OnEnable()
        {
            _fishesManager.FishTransferred += SaveCounters;
            _resourcesManager.ResourceCountChanged += SaveCounters;
        }

        private void OnDisable()
        {
            _fishesManager.FishTransferred -= SaveCounters;
            _resourcesManager.ResourceCountChanged -= SaveCounters;
        }

        private void SaveCounters()
        {
            YandexGame.savesData.SaveFishesCountData(_fishesManager.CatchedFishes);
            YandexGame.savesData.SaveResourcesCountData(_resourcesManager.ResCounters);
        }
    }
}