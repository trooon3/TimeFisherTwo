using UnityEngine;

namespace Assets.Scripts.Fishes
{
    [CreateAssetMenu(fileName = "ShopAtribut", menuName = "ShopAtributs/FishCountPrice")]
    public class FishCountPrice : ScriptableObject
    {
        [SerializeField] private SeaCreature _creature;
        [SerializeField] private int _cost;
        private FishType _type;
        private Sprite _icon;

        public Sprite Icon => _icon;
        public FishType Type => _type;
        public int Cost => _cost;

        private void Awake()
        {
            _type = _creature.FishType;
            _icon = _creature.Icon;
        }
    }
}

