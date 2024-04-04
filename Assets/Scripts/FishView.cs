using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishView : MonoBehaviour
{
    private TMP_Text _label;
    private TMP_Text _count;
    private Image _icon;
    private Button _hook;
    private SeaCreature _fish;
    private Closet _closet;

    private void Render(SeaCreature fish)
    {
        _fish = fish;

        _label.text = fish.FishType.ToString();
        _icon.sprite = fish.Icon;
    }
}
