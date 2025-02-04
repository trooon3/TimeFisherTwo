using UnityEngine;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Fishes
{
    [Serializable]
    public class FishTypeCounter
    {
        [SerializeField] private FishType _type;
        [SerializeField] [Min(0)] private int _count;

        public FishType Type => _type;
        public int Count => _count;

        public UnityAction CountChanged;

        public FishTypeCounter(FishType type)
        {
            _type = type;
            _count = 0;
        }

        public void Increase()
        {
            _count++;
        }

        public void Decrease()
        {
            if (_count > 0)
            {
                _count--;
            }
        }
    }
}

