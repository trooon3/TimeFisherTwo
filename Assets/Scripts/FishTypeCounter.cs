using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTypeCounter : MonoBehaviour
{
    private FishType _type;
    [Min(0)] private int _count;

    public FishType Type => _type;
    public int Count => _count;

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
