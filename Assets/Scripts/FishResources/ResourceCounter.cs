using UnityEngine;
using System;

namespace Assets.Scripts.FishResources
{
    [Serializable]
    public class ResourceCounter
    {
        [SerializeField] private Resource ThisResource;
        [Min(0)] [SerializeField] private int ResourceCount;

        public Resource Resource => ThisResource;
        public int Count => ResourceCount;

        public ResourceCounter(Resource type)
        {
            ThisResource = type;
            ResourceCount = 0;
        }

        public void Increase()
        {
            ResourceCount++;
        }

        public void Decrease()
        {
            if (ResourceCount > 0)
            {
                ResourceCount--;
            }
        }
    }
}

