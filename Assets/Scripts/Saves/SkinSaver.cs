using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSaver : MonoBehaviour
{
    public string CreateJson(SkinView skin)
    {
        string save = JsonUtility.ToJson(skin);
        return save;
    }
}
