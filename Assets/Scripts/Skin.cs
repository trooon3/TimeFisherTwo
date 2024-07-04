using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private SkinCost _cost;
    private Animator _animator;
    public SkinCost Cost => _cost;
    public Sprite Icon => _icon;
    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
