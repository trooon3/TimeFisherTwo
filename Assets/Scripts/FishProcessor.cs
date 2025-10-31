using System.Collections.Generic;
using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using UnityEngine;

namespace Assets.Scripts
{
    public class FishProcessor : MonoBehaviour
    {
        [SerializeField] private FishSpawner _spawner;
        [SerializeField] private ResourcesManager _resourcesManager;
        [SerializeField] private FishesManager _fishesManager;

        public void ProcessFishes(List<Fish> fishes)
        {
            foreach (var fish in fishes)
            {
                _fishesManager.AddFish(fish);
                _resourcesManager.AddResources(fish);
                _spawner.SetOffFish(fish);
            }
        }
    }
}