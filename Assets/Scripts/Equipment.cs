using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Fishes;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves;
using Assets.Scripts.Saves.DTO;
using UnityEngine;
using UnityEngine.Events;

public abstract class Equipment : MonoBehaviour
{
    protected const int ZeroLevelCommand = 0;
    protected const int FirstLevelCommand = 1;
    protected const int SecondLevelCommand = 2;
    protected const int ThirdLevelCommand = 3;
    protected const int FourthLevelCommand = 4;

    protected const int ZeroLevelCost = 10;
    protected const int FirstLevelCost = 25;
    protected const int SecondLevelCost = 50;
    protected const int ThirdLevelCost = 75;
    protected const int FourthLevelCost = 100;

   // protected DataSaver _saver;
    protected readonly int _maxLevel = 5;
    private readonly string _levelDataKey;
    protected int _level;
    protected Resource _resourceToUpgrade;
    protected int _upgradeCost;
    private float _upgradeParametr;

    public string LevelDataKey => _levelDataKey;
    public string NextLevel { get; protected set; }
    public Resource ResourceToUpgrade => _resourceToUpgrade;

    public event UnityAction Upgraded;

    public void Upgrade(string levelDataKey)
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

       SaveAfterUpdate();
    }

    private void SaveAfterUpdate()
    {
        // CheckLevel();
        // DTOLevel dTOLevel = new DTOLevel();
        // dTOLevel.Init(_upgradeCost, _level);
        // _saver.SaveLevelData(levelDataKey, dTOLevel);
    }

    public abstract void CheckLevel();

    protected void SmartCheckLevel(int level, UpgradeCriterion[] criteria, ref int upgradeCost, ref float upgradeParametr)
    {
        upgradeCost = criteria[level].Cost;
        upgradeParametr = criteria[level].Parametr;
    }

    public Resource GetResourceToUpgrade()
    {
        return ResourceToUpgrade;
    }

    public int GetResourceCountToUpgrade()
    {
        return _upgradeCost;
    }
}
