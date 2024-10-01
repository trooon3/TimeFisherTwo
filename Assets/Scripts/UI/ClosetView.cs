using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClosetView : MonoBehaviour
{
    [SerializeField] private Closet _closet;
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

        foreach (var creature in _closet.AllFishes)
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
        _closet.FishTransferred += RefreshFishCounts;
        _closet.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _closet.FishTransferred -= RefreshFishCounts;
        _closet.ResourceCountChanged -= OnResourceCountChanged;
    }

    public void OnResourceCountChanged()
    {
        _weedCount.text = _closet.GetSeaWeedCount().ToString();
        _boneCount.text = _closet.GetFishBonesCount().ToString();
    }

    public void OnHookButtonClick(FishType type)
    {
        _closet.RemoveFish(type);
        _rod.GetReadyCatch(type);
        SetButtonsActive(false);
    }

    public void AddFishAndRefresh()
    {
        _closet.AddFishOnRod(_rod.FishFoodFor);
        SetButtonsActive(true);
        RefreshFishCounts();
    }

    public void SetCounters()
    {
        foreach (var card in _fishCardViewers)
        {
            foreach (var counter in _closet.CatchedFishes)
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
