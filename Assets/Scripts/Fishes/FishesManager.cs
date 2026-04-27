using System.Collections.Generic;
using Assets.Scripts.Saves;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Assets.Scripts.Fishes
{
    [JsonObject(MemberSerialization.Fields)]
    public class FishesManager : MonoBehaviour
    {
        [SerializeField] private List<SeaCreature> _allFishes = new List<SeaCreature>();
        [SerializeField] private List<FishTypeCounter> _catchedFishes;

        public List<FishTypeCounter> CatchedFishes => _catchedFishes;
        public List<SeaCreature> AllFishes => _allFishes;

        public event UnityAction FishTransferred;

        private void Awake()
        {
            YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialRod);
            LoadCount();

            if (_catchedFishes == null)
            {
                _catchedFishes = new List<FishTypeCounter>();

                foreach (var creature in _allFishes)
                {
                    FishTypeCounter counter = new FishTypeCounter(creature.FishType);

                    if (!_catchedFishes.Contains(counter))
                    {
                        _catchedFishes.Add(counter);
                    }
                }
            }
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadCount;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadCount;
        }

        public void RemoveFish(FishType fish)
        {
            foreach (FishTypeCounter item in _catchedFishes)
            {
                if (item.Type == fish)
                {
                    item.Decrease();
                    FishTransferred?.Invoke();
                }
            }

            YandexGame.savesData.SaveFishesCountData(_catchedFishes);
        }

        public void AddFish(FishType type)
        {
            foreach (FishTypeCounter catchedFish in _catchedFishes)
            {
                if (catchedFish.Type == type)
                {
                    catchedFish.Increase();
                    FishTransferred?.Invoke();
                }
            }
        }

        public void AddFish(Fish fish) => AddFish(fish.Type);

        public void GetAllFishesCHEAT()
        {
            foreach (var fish in _catchedFishes)
            {
                for (int i = 0; i < 1000; i++)
                {
                    fish.Increase();
                }
            }
        }

        private void LoadCount()
        {
           _catchedFishes = YandexGame.savesData.LoadFishesCountData();
        }
    }
}