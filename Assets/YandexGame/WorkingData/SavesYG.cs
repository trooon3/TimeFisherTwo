using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        [SerializeField] private SkinNames _chosenSkin;
        [SerializeField] private int _level = 0;

        public Dictionary<TutorialsKeys, bool> _tutorialSaves = new();
        public Dictionary<SkinNames, bool> _skinKeys = new();

        [SerializeField] private List<FishTypeCounter> _fishCounters;
        [SerializeField] private List<ResourceCounter> _resCounters;

        public List<FishTypeCounter> FishCounters => _fishCounters;
        public List<ResourceCounter> ResCounters => _resCounters;
        public int Level => _level;
        // Вы можете выполнить какие то действия при загрузке сохранений

        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            
            openLevels[1] = true;
        }

        public void SaveTutorial(TutorialsKeys key ,bool value)
        {
            if (_tutorialSaves.ContainsKey(key))
            {
                _tutorialSaves[key] = value;
                YandexGame.SaveProgress();
            }
            else
            {
                _tutorialSaves.Add(key, value);
                YandexGame.SaveProgress();
            }
        }

        public bool LoadTutorial(TutorialsKeys key)
        {
            if (_tutorialSaves.ContainsKey(key))
            {
                return _tutorialSaves[key];
            }
            else
            {
                _tutorialSaves.Add(key, false);
                return false;
            }
        }

        public void SaveSkins(SkinNames name, bool value)
        {
            if (_skinKeys.ContainsKey(name))
            {
                _skinKeys[name] = value;
                YandexGame.SaveProgress();
            }
            else
            {
                _skinKeys.Add(name, value); 
                YandexGame.SaveProgress();
            }
        }

        public bool LoadSkinsSaves(SkinNames key)
        {
            if (_skinKeys.ContainsKey(key))
            {
                return _skinKeys[key];
            }
            else
            {
                _skinKeys.Add(key, false);
                return false;
            }
        }

        public void SaveChosenSkin(SkinNames skin)
        {
            _chosenSkin = skin;
            YandexGame.SaveProgress();
        }

        public SkinNames LoadChosenSkin()
        {
            return _chosenSkin;
        }

        public void SaveLevel(int level)
        {
            _level = level;
            YandexGame.SaveProgress();
        }

        public int LoadLevel()
        {
            return Level;
        }

        public void SaveFishesCountData(List<FishTypeCounter> counters)
        {
            _fishCounters = counters;
            YandexGame.SaveProgress();
        }

        public List<FishTypeCounter> LoadFishesCountData()
        {
            if (_fishCounters != null)
            {
                return FishCounters;
            }
            else
            {
                return null;
            }
        }

        public void SaveResourcesCountData(List<ResourceCounter> counters)
        {
            _resCounters = counters;
            YandexGame.SaveProgress();
        }

        public List<ResourceCounter> LoadResourcesCountData()
        {
            if (_resCounters == null || _resCounters.Count == 0)
            {
                SaveResourcesCountData(new List<ResourceCounter>
                {
                    new ResourceCounter(Resource.FishBones),
                    new ResourceCounter(Resource.SeaWeed)
                });
            }

            return _resCounters;
        }
    }
}