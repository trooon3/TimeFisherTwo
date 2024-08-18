using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private SkinCost _cost;
    [SerializeField] private string _name;

    private Animator _animator;

    public SkinCost Cost => _cost;
    public string Name => _name;
    public Sprite Icon => _icon;
    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
