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
}
