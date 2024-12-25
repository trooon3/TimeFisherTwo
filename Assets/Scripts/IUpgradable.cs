using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IUpgradable
    {
        public void Upgrade();
        public Resource GetResourceToUpgrade();
        public int GetResourceCountToUpgrade();
    }
}

