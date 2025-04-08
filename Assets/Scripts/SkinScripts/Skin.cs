using Assets.Scripts.Saves;
using UnityEngine;

namespace Assets.Scripts.SkinScripts
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private SkinCost _cost;
        [SerializeField] private SkinNames _name;

        private Animator _animator;

        public SkinCost Cost => _cost;
        public SkinNames Name => _name;
        public Sprite Icon => _icon;
        public Animator Animator => _animator;

        private void Awake()
        {
            _animator = gameObject.GetComponentElseThrow<Animator>();
        }
    }
}

