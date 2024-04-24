using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(FishMover))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animation))]
//[RequireComponent(typeof(FishCatchTimerViewer))]

public class Fish : MonoBehaviour
{
    [SerializeField] private SeaCreature _creature;

    //private FishCatchTimerViewer _fishCatchTimerViewer;
    private Resource _resource;
    private FishType _type;
    private Sprite _icon;
    private string _name;
    private int _level;

    public Resource Resource => _resource;
    public FishType Type => _type;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Level => _level;

    public float Catchtime { get; private set; }
    public UnityAction Catched;

    private void Start()
    {
       // _fishCatchTimerViewer = GetComponent<FishCatchTimerViewer>();
        Catchtime = 1f;
        Init(_creature);
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
