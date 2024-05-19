using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBrench : MonoBehaviour
{
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    [SerializeField] private Closet _closet;
    [SerializeField] private List<GameObject> _tools;
    [SerializeField] private WorckBranchViewer _viewer;
    private Player _player;

    private void Update()
    {
        if (_playerNearbyChecker.IsPlayerNearby)
        {
            _viewer.gameObject.SetActive(true);

            if (_playerNearbyChecker.GetPlayer() != null)
            {
                _player = _playerNearbyChecker.GetPlayer();
            }
        }
        else
        {
            _viewer.gameObject.SetActive(false);
        }
    }

    public void TryUpgrade(IUpgradable tool)
    {
        var needResource = tool.GetResourceToUpgrade();
        var needCountResource = tool.GetResourceCountToUpgrade();

        if (_closet.CheckIsCanPay(needResource, needCountResource))
        {
            _closet.SpendResources(needCountResource, needResource);
            tool.Upgrade();
        }
    }

}
