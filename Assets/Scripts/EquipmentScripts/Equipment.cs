using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Assets.Scripts.EquipmentScripts
{
    public abstract class Equipment : MonoBehaviour
    {
        private readonly int _maxLevel = 5;
        private int _level;
        private int _upgradeCost;
        private float _upgradeParametr;
        private Resource _resourceToUpgrade;

        public int Level { get { return _level; } }
        public int UpgradeCost => _upgradeCost;
        public float UpgaradeParametr => _upgradeParametr;
        public Resource ResourceToUpgrade => _resourceToUpgrade;

        public string NextLevel { get; protected set; }
        public event UnityAction Upgraded;

        public void Upgrade()
        {
            if (_level < _maxLevel)
            {
                _level++;

                if (_level + 1 > _maxLevel)
                {
                    NextLevel = "MAX";
                }
                else
                {
                    NextLevel = (_level + 1).ToString();
                }

                Upgraded?.Invoke();
            }

            CheckLevel();
        }

        public abstract void CheckLevel();

        public Resource GetResourceToUpgrade()
        {
            return ResourceToUpgrade;
        }

        public int GetResourceCountToUpgrade()
        {
            return _upgradeCost;
        }

        protected void SetLevel(int level)
        {
            if (level >= 1 && level <= 5)
            {
                _level = level;
                CheckLevel();
            }
        }

        protected void SetResourceType(Resource resource)
        {
            _resourceToUpgrade = resource;
        }

        protected void SmartCheckLevel(int level, UpgradeCriterion[] criteria, 
                                       ref int upgradeCost, ref float upgradeParametr)
        {
            upgradeCost = criteria[level].Cost;
            upgradeParametr = criteria[level].Parametr;
            YandexGame.savesData.SaveLevel(level);
        }
    }
}