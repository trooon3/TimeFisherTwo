using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetView : MonoBehaviour
{
    [SerializeField] private Closet _closet;
    [SerializeField] private FishCardViewer _template;
    [SerializeField] private Transform _container;
    private List<FishCardViewer> _fishCardViewer;

    private void Start()
    {
        _fishCardViewer = new List<FishCardViewer>();

        foreach (var creature in _closet.AllFishes)
        {
            FishCardViewer fishCardViewer = Instantiate(_template, _container);
            fishCardViewer.Init(creature);
            _fishCardViewer.Add(fishCardViewer);
        }

        SetCounters();
    }

    public void SetCounters()
    {
        foreach (var card in _fishCardViewer)
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
        foreach (var card in _fishCardViewer)
        {
            card.RefreshCount();
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
}
