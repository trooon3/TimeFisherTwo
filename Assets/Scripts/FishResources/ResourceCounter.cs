using UnityEngine;
using System;
using Newtonsoft.Json;

namespace Assets.Scripts.FishResources
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class ResourceCounter
    {
        [SerializeField] private Resource _thisResource;
        [Min(0)] [SerializeField] private int _resourceCount;

        public Resource Resource => _thisResource;
        public int Count => _resourceCount;

        public ResourceCounter(Resource type)
        {
            _thisResource = type;
            _resourceCount = 0;
        }

        public void Increase()
        {
            _resourceCount++;
        }

        public void Decrease()
        {
            if (_resourceCount > 0)
            {
                _resourceCount--;
            }
        }
    }
}

