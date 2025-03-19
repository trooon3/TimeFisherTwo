using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Saves
{
    [Serializable]
    public class SaveData
    {
        private List<FishTypeCounter> _fishCounters = new();
        private List<ResourceCounter> _resCounters = new();

        public List<FishTypeCounter> FishCounters => _fishCounters;
        public List<ResourceCounter> ResCounters => _resCounters;

        public void SetFishCounters(List<FishTypeCounter> fishCounters)
        {
            _fishCounters = fishCounters;
        }

        public void SetResCounters(List<ResourceCounter> resCounters) 
        {
            _resCounters = resCounters;
        }
    }
}

   
