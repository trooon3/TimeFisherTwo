using System;
using System.Collections.Generic;

namespace Assets.Scripts.Saves
{
    [Serializable]
    public class SaveData
    {
        public List<FishTypeCounter> FishCounters = new();
        public List<ResourceCounter> ResCounters = new();

    }
}

   
