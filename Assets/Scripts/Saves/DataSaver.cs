using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public void SaveFishesCountData(string key, List<FishTypeCounter> counters)
    {
        SaveData saveData = new();
        saveData.FishCounters = counters;
        var jsonString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }

    public List<FishTypeCounter> LoadFishesCountData(string key)
    {
        var jsonString = PlayerPrefs.GetString(key);
        var saveData = JsonUtility.FromJson<SaveData>(jsonString);

        if (saveData != null)
        {
            return saveData.FishCounters;
        }
        else
        {
            return null;
        }
    }

    public void SaveResourcesCountData(string key, List<ResourceCounter> counters)
    {
        SaveData saveData = new();
        saveData.ResCounters = counters;
        var jsonString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }

    public List<ResourceCounter> LoadResourcesCountData(string key)
    {
        var jsonString = PlayerPrefs.GetString(key);
        var saveData = JsonUtility.FromJson<SaveData>(jsonString);

        if (saveData != null)
        {
            return saveData.ResCounters;
        }
        else
        {
            return new List<ResourceCounter> { new ResourceCounter(Resource.FishBones), new ResourceCounter(Resource.SeaWeed) };
        }
    }

    public void SaveSkinData(string key, DTOSkin dtoSkin )
    {
        var jsonString = JsonUtility.ToJson(dtoSkin);
        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }

     public DTOSkin LoadSkinData(string key)
     {
        var jsonString = PlayerPrefs.GetString(key);
        var dtoSkin = JsonUtility.FromJson<DTOSkin>(jsonString);

        if (dtoSkin != null)
        {
            return dtoSkin;
        }

        return new DTOSkin();
     }

    public void SaveLevel(string key, int level)
    {
        PlayerPrefs.SetInt(key, level);
        PlayerPrefs.Save();
    }

    public int LoadLevel(string key)
    {
        return PlayerPrefs.GetInt(key, 0);
    }

    public void SaveChosenSkin(string key, string skinName)
    {
        PlayerPrefs.SetString(key, skinName);
        PlayerPrefs.Save();
    }

    public string LoadChosenSkin(string key)
    {
        return PlayerPrefs.GetString(key, null);
    }
}
