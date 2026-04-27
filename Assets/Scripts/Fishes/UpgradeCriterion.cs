using UnityEngine;

namespace Assets.Scripts.Fishes
{
    [CreateAssetMenu(fileName = "Fish", menuName = "UpgradeCriterion")]
    public class UpgradeCriterion : ScriptableObject
    {
        [SerializeField] private int _cost;
        [SerializeField] private float _parametr;

        public int Cost => _cost;
        public float Parametr => _parametr;
    }
}
