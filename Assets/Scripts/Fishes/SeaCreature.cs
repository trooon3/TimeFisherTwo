using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Fish", menuName = "SeaCreature")]
public class SeaCreature : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private FishType _fishType;
    [SerializeField] private SeaCreature _foodFor;
    [SerializeField] private Sprite _icon;
    [SerializeField, Range(1,5)] private int _level;
    [SerializeField] Resource _resource;

     public Resource Resource => _resource;
     public FishType FishType => _fishType;
     public SeaCreature FoodFor => _foodFor;
     public string Name => _name;
     public Sprite Icon => _icon;
     public int Level => _level;

}

