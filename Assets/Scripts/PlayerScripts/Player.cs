using Assets.Scripts.Fishes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof(Bag))]
    public class Player : MonoBehaviour
    {
        private Bag _bag;

        void Start()
        {
            _bag = GetComponent<Bag>();
        }

        public List<Fish> GetFish()
        {
            return _bag.GetFish();
        }
    }
}

