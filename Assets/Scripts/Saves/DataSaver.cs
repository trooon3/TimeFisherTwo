using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves.DTO;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Saves
{
    public class DataSaver : MonoBehaviour
    {
        public void SaveFishesCountData(string key, List<FishTypeCounter> counters)
        {
            SaveData saveData = new();
            saveData.SetFishCounters(counters);
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
            saveData.SetResCounters(counters);
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

        public void SaveSkinData(string key, DTOSkin dtoSkin)
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

        public void SaveUpgradeCost(string key, int cost)
        {
            PlayerPrefs.SetInt(key, cost);
            PlayerPrefs.Save();
        }

        public int LoadUpgradeCost(string key)
        {
            return PlayerPrefs.GetInt(key, 10);
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

        public DTOLevel LoadLevelData(string key)
        {
            var jsonString = PlayerPrefs.GetString(key);
            var dtoLevelData = JsonUtility.FromJson<DTOLevel>(jsonString);

            if (dtoLevelData != null)
            {
                return dtoLevelData;
            }
            DTOLevel levelData = new DTOLevel();
            levelData.Init(0, 10, 0);
            return levelData;
        }

        public void SaveLevelData(string key, DTOLevel dtoLevelData)
        {
            var jsonString = JsonUtility.ToJson(dtoLevelData);
            PlayerPrefs.SetString(key, jsonString);
            PlayerPrefs.Save();
        }

        public void SaveTutorialData(string key, DTOTutorial dtoTutorial)
        {
            var jsonString = JsonUtility.ToJson(dtoTutorial);
            PlayerPrefs.SetString(key, jsonString);
            PlayerPrefs.Save();
        }

        public DTOTutorial LoadTutorialData(string key)
        {
            var jsonString = PlayerPrefs.GetString(key);
            var dtoTutorial = JsonUtility.FromJson<DTOTutorial>(jsonString);

            if (dtoTutorial != null)
            {
                return dtoTutorial;
            }

            return new DTOTutorial();
        }

        public void SaveTutorialDirectonGuideData(string key, DTODirectionGuide dtoTutorial)
        {
            var jsonString = JsonUtility.ToJson(dtoTutorial);
            PlayerPrefs.SetString(key, jsonString);
            PlayerPrefs.Save();
        }

        public DTODirectionGuide LoadTutorialDirectonGuideData(string key)
        {
            var jsonString = PlayerPrefs.GetString(key);
            var dtoTutorial = JsonUtility.FromJson<DTODirectionGuide>(jsonString);

            if (dtoTutorial != null)
            {
                return dtoTutorial;
            }

            return new DTODirectionGuide();
        }
    }
}