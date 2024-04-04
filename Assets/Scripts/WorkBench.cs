using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBench : MonoBehaviour
{
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    private Player _player;
    private List<IUpgradable> _tools;

    private void Start()
    {

    }
    
    private void Update()
    {
        //if (_playerNearbyChecker.IsPlayerNearby && Input.GetKeyDown(KeyCode.E))
        //{
        //    // открыть UI меню и там уже вызвать методы далее.
        //}
    }

    private void TryUpgrade(IUpgradable tool)
    {
        if (_player != null)
        {
            var needResource = tool.GetResourceToUpgrade();
            var needCountResource = tool.GetResourceCountToUpgrade();

            if (_player.IsCanPay(needResource, needCountResource))
            {
                _player.SpendResources(needCountResource, needResource);
                tool.Upgrade();
            }
        }
    }
}
