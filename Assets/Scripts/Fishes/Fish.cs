using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(FishMover))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animation))]

public class Fish : MonoBehaviour
{
    [SerializeField] private SeaCreature _creature;

    private string _name;
    private FishType _type;
    private Sprite _icon;
    private int _level;
    private Resource _resource;

    public Resource Resource => _resource;
    public FishType Type => _type;
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Level => _level;

    public float Catchtime { get; private set; }
    public UnityAction Catched;

    private void Start()
    {
        Catchtime = 2f;
    }

    public void Init(SeaCreature seaCreature)
    {
        _resource = seaCreature.Resource;
        _name = seaCreature.Name;
        _level = seaCreature.Level;
        _type = seaCreature.FishType;
        _icon = seaCreature.Icon;
    }

    public void SetOffFish()
    {
        Catched?.Invoke();
        Debug.Log("SetoffFish сработал");
        gameObject.SetActive(false);
    }
}
