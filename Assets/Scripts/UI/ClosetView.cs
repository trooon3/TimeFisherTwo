using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.RodScripts;
using Assets.Scripts.SkinScripts;
using Assets.Scripts.Tutorial;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ClosetView : MonoBehaviour
    {
        [SerializeField] private Chest _closet;
        [SerializeField] private ResourcesManager _resourcesManager;
        [SerializeField] private FishesManager _fishesManager;
        [SerializeField] private Rod _rod;
        [SerializeField] private FishCardViewer _template;
        [SerializeField] private Transform _container;
        [SerializeField] private TutorialViewer _tutorial;

        [SerializeField] private TMP_Text _boneCount;
        [SerializeField] private TMP_Text _weedCount;

        private List<FishCardViewer> _fishCardViewers;

        private void Awake()
        {
            _fishCardViewers = new List<FishCardViewer>();

            foreach (var creature in _fishesManager.AllFishes)
            {
                FishCardViewer fishCardViewer = Instantiate(_template, _container);
                fishCardViewer.Init(creature);
                fishCardViewer.SetCloset(this);
                _fishCardViewers.Add(fishCardViewer);
            }

            foreach (var fishCard in _fishCardViewers)
            {
                fishCard.gameObject.SetActive(false);
            }

            OnResourceCountChanged();
            SetCounters();
        }

        private void Start()
        {
            RefreshFishCounts();
        }

        private void OnEnable()
        {
            _fishesManager.FishTransferred += RefreshFishCounts;
            _resourcesManager.ResourceCountChanged += OnResourceCountChanged;
        }

        private void OnDisable()
        {
            _fishesManager.FishTransferred -= RefreshFishCounts;
            _resourcesManager.ResourceCountChanged -= OnResourceCountChanged;
        }

        public void OnResourceCountChanged()
        {
            _weedCount.text = _resourcesManager.GetSeaWeedCount().ToString();
            _boneCount.text = _resourcesManager.GetFishBonesCount().ToString();
        }

        public void OnHookButtonClick(FishType type)
        {
            _fishesManager.RemoveFish(type);
            _rod.GetReadyCatch(type);
            SetButtonsActive(false);
        }

        public void AddFishAndRefresh()
        {
            _fishesManager.AddFish(_rod.FishFoodFor);
            SetButtonsActive(true);
            RefreshFishCounts();
        }

        public void SetCounters()
        {
            foreach (var card in _fishCardViewers)
            {
                foreach (var counter in _fishesManager.CatchedFishes)
                {
                    if (counter.Type == card.SeaCreature.FishType)
                    {
                        card.SetCounter(counter);
                    }
                }
            }
        }

        private void RefreshFishCounts()
        {
            foreach (var card in _fishCardViewers)
            {
                card.RefreshCount();

                if (card.GetCount() > 0)
                {
                    card.gameObject.SetActive(true);
                }
            }
        }

        private void SetButtonsActive(bool active)
        {
            foreach (var fish in _fishCardViewers)
            {
                fish.SetActiveHookButton(active);
            }
        }
    }
}