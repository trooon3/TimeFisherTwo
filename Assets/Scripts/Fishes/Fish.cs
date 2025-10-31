using Assets.Scripts.FishResources;
using UnityEngine;

namespace Assets.Scripts.Fishes
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(FishMover))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animation))]
    [RequireComponent(typeof(FishCatchTimer))]
    
    public class Fish : MonoBehaviour
    {
        [SerializeField] private SeaCreature _creature;

        private FishCatchTimer _catchTimer;
        private Resource _resource;
        private FishType _type;
        private Sprite _icon;
        private string _name;
        private int _level;
        private float _catchTime;

        public Resource Resource => _resource;
        public FishType Type => _type;
        public float CatchTime => _catchTime;
        public FishCatchTimer CatchTimer => _catchTimer;

        private void Start()
        {
            Init(_creature);
            _catchTimer = GetComponent<FishCatchTimer>();
        }

        public void Init(SeaCreature seaCreature)
        {
            _catchTime = seaCreature.CatchTime;
            _resource = seaCreature.Resource;
            _name = seaCreature.Name;
            _level = seaCreature.Level;
            _type = seaCreature.FishType;
            _icon = seaCreature.Icon;
        }
    }
}