using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountViewer : MonoBehaviour
{
    [SerializeField] private Closet _closet;
    [SerializeField] private Bag _bag;
    [SerializeField] private TMP_Text _fishCount;
    [SerializeField] private TMP_Text _boneCount;
    [SerializeField] private TMP_Text _weedCount;

    private void Start()
    {
        OnFishAdded();
        OnResourceAdded();
    }

    private void OnEnable()
    {
        _bag.FishCountChanged += OnFishAdded;
        _closet.ResourceCountChanged += OnResourceAdded;
    }

    private void OnDisable()
    {
        _bag.FishCountChanged -= OnFishAdded;
        _closet.ResourceCountChanged -= OnResourceAdded;
    }

    private void OnFishAdded()
    {
        _fishCount.text = _bag.FishesInsideCount.ToString();
    }

    private void OnResourceAdded()
    {
        _weedCount.text = _closet.GetSeaWeedCount().ToString();
        _boneCount.text = _closet.GetFishBonesCount().ToString();
    }
}
