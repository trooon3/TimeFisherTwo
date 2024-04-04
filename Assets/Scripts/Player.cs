using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int GetNeedResourceCount(IUpgradable tool)
    {
        return _bag.GetNeedResourceCount(tool.GetResourceToUpgrade());
    }

    public void SpendResources(int count, Resource type)
    {
        _bag.SpendResources(count, type);
    }

    public bool IsCanPay(Resource type, int count)
    {
       return _bag.CheckIsCanPay(type, count);
    }
}
