using System.Collections.Generic;
using Assets.Scripts.Fishes;
using Assets.Scripts.Saves;
using Assets.Scripts.Tutorial;
using UnityEngine;
using UnityEngine.Events;
using YG;
using Assets.Scripts.UI;
using Assets.Scripts.BagScripts;

namespace Assets.Scripts.Fishes
{
    public class FishStorage : MonoBehaviour
    {
        [SerializeField] private int _maxFishCount;
        [SerializeField] private BagAdBoostController _adBoostController;
        [SerializeField] private TutorialViewer _tutorial;
        private List<Fish> _fishes = new List<Fish>();
        public int CurrentCount => _fishes.Count;
        public int MaxCapacity => _maxFishCount;

        public event UnityAction BagDevastated;
        public event UnityAction FishCountChanged;
        public event UnityAction BagFilled;

        public bool TryAddFish(Fish fish)
        {
            if (_fishes.Count >= _maxFishCount)
                return false;

            _fishes.Add(fish);
            FishCountChanged?.Invoke();

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
            {
                _tutorial.ChangeState<ShowWhereFishesCountTutorialState>();
            }

            if (_fishes.Count == _maxFishCount)
                BagFilled?.Invoke();

            return true;
        }

        public List<Fish> GetFish()
        {
            var fishes = new List<Fish>();

            foreach (var fish in _fishes)
            {
                if (_adBoostController.IsBoostActive)
                {
                    fishes.Add(fish);
                }

                fishes.Add(fish);
            }

            _fishes.Clear();
            BagDevastated?.Invoke();
            FishCountChanged?.Invoke();
            return fishes;
        }
    }
}