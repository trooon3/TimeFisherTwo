using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
   [SerializeField] private Resource _resource;
    private int _count;

    public Resource Resource => _resource;
    private int Count => _count;

    public ResourceCounter(Resource type)
    {
        _resource = type;
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
