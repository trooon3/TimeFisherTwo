using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    public void Upgrade();
    public Resource GetResourceToUpgrade();
    public int GetResourceCountToUpgrade();
}
