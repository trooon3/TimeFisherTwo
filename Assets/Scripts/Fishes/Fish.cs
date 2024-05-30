using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(FishMover))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(FishCatchTimerViewer))]

public class Fish : MonoBehaviour
{
    [SerializeField] private SeaCreature _creature;

    private FishCatchTimerViewer _fishCatchTimerViewer;
    private Resource _resource;
    private FishType _type;
    private Sprite _icon;
    private string _name;
    private int _level;
    private float _catchtime;

    public Resource Resource => _resource;
    public FishType Type => _type;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Level => _level;
    public float Catchtime => _catchtime;

    private void Start()
    {
        _fishCatchTimerViewer = GetComponent<FishCatchTimerViewer>();
        Init(_creature);
    }

    public void Init(SeaCreature seaCreature)
    {
        _catchtime = seaCreature.Catchtime;
        _resource = seaCreature.Resource;
        _name = seaCreature.Name;
        _level = seaCreature.Level;
        _type = seaCreature.FishType;
        _icon = seaCreature.Icon;
    }

    public void StartChangeTimerValue()
    {
        _fishCatchTimerViewer.StartDisplayCatching();
    }

    public void SetCatcher(FishCatcher catcher)
    {
        _fishCatchTimerViewer.SetCatcher(catcher);
    }
}
