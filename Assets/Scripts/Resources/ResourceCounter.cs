using UnityEngine;
using System;

[Serializable]
public class ResourceCounter
{
    private Resource _resource;
    [Min(0)] private int _count;

    public Resource Resource => _resource;
    public int Count => _count;

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
