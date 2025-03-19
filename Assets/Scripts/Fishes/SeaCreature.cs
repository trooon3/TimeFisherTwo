using Assets.Scripts.FishResources;
using UnityEngine;

namespace Assets.Scripts.Fishes
{
    [CreateAssetMenu(fileName = "Fish", menuName = "SeaCreature")]
    public class SeaCreature : ScriptableObject
    {
        [SerializeField, Range(1, 5)] private int _level;
        [SerializeField] private string _name;
        [SerializeField] private float _catchTime;
        [SerializeField] private FishType _fishType;
        [SerializeField] private SeaCreature _foodFor;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Resource _resource;

        public float CatchTime => _catchTime;
        public string Name => Lean.Localization.LeanLocalization.GetTranslationText(_name);
        public int Level => _level;
        public FishType FishType => _fishType;
        public SeaCreature FoodFor => _foodFor;
        public Sprite Icon => _icon;
        public Resource Resource => _resource;
    }
}