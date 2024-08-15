using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    public string CreateJson(int level)
    {
        string save = JsonUtility.ToJson(level);
        return save;
    }
}
