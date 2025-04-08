using System;
using UnityEngine;

public static class GameObjectExtentions
{
    public static T GetComponentElseThrow<T>(this GameObject target) 
        where T : Component
    {
        if (target == null)
            throw new ArgumentNullException("target");
        if (target.TryGetComponent(out T result))
        {
            return result;
        }
        else
        {
            throw new Exception("Component no found");
        }
    }
}
