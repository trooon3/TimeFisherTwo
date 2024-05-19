using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetView : MonoBehaviour
{
    [SerializeField] private Closet _closet;
    [SerializeField] private FishCardViewer _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Rod _rod;
    private List<FishCardViewer> _fishCardViewers;

    private void Start()
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

        SetCounters();
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

    private void OnEnable()
    {
        _closet.FishTransferred += RefreshFishCounts;
    }

    private void OnDisable()
    {
        _closet.FishTransferred -= RefreshFishCounts;

    }

    public void OnHookButtonClick(FishType type)
    {
        _closet.RemoveFish(type);
        _closet.AddFishOnRod(_rod.GetFishFoodFor(type));
        RefreshFishCounts();
    }
}
