using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
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

       // CheckLevel(ref _upgradeCost, ref _upgradeParametr);
       // DTOLevel dTOLevel = new DTOLevel();
       // dTOLevel.Init(_upgradeCost, _level);
       //// _saver.SaveLevelData(levelDataKey, dTOLevel);
    }

    protected void CheckLevel(ref int upgradeCost, ref float upgradeParametr) 
    {
        switch (_level)
        {
            case ZeroLevelCommand:
                upgradeCost = 10;
                upgradeParametr = 0.01f;
                break;

            case FirstLevelCommand:
                upgradeCost = 25;
                upgradeParametr = 0.02f;
                break;

            case SecondLevelCommand:
                upgradeCost = 50;
                upgradeParametr = 0.04f;
                break;

            case ThirdLevelCommand:
                upgradeCost = 75;
                upgradeParametr = 0.1f;
                break;

            case FourthLevelCommand:
                upgradeCost = 100;
                upgradeParametr = 0.2f;
                break;

            default:
                break;
        }
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
