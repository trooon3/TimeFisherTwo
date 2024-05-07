using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishCardViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _hook;

    private SeaCreature _seaCreature;
    private FishTypeCounter _counter;
    public SeaCreature SeaCreature => _seaCreature;

    public void Init(SeaCreature fish)
    {
        _seaCreature = fish;

        _label.text = fish.FishType.ToString();
        _icon.sprite = fish.Icon;
    }

    public void SetCounter(FishTypeCounter counter)
    {
        _counter = counter;
        RefreshCount();
    }

    public void RefreshCount()
    {
        _count.text = _counter.Count.ToString();
    }
}
