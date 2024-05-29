using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagCountViewer : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private TMP_Text _fishCount;
    [SerializeField] private TMP_Text _filledBagText;

    private void Start()
    {
        OnFishAdded();
        OnBagEmpty();
    }

    private void OnEnable()
    {
        _bag.FishCountChanged += OnFishAdded;
        _bag.BagFilled += OnBagFilled;
        _bag.BagDevastated += OnBagEmpty;
    }

    private void OnDisable()
    {
        _bag.FishCountChanged -= OnFishAdded;
        _bag.BagFilled -= OnBagFilled;
        _bag.BagDevastated -= OnBagEmpty;
    }

    private void OnFishAdded()
    {
        _fishCount.text = _bag.FishesInsideCount.ToString();
    }

    private void OnBagFilled()
    {
        _filledBagText.gameObject.SetActive(true);
    }

    private void OnBagEmpty()
    {
        _filledBagText.gameObject.SetActive(false);
    }
}
