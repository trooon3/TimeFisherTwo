using System.Collections.Generic;
using UnityEngine;

public class WorkBrench : MonoBehaviour
{
    [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
    [SerializeField] private Closet _closet;
    [SerializeField] private List<GameObject> _tools;
    [SerializeField] private WorkBranchViewer _viewer;
    [SerializeField] private ActiveButtonView _buttonView;
    [SerializeField] private TutorialViewer _tutorial;

    private Player _player;
    private bool _isTutorialShowed;

    private void Update()
    {
        if (_playerNearbyChecker.IsPlayerNearby)
        {
            _buttonView.SetActiveEImage(true);
        }
        else
        {
            _buttonView.SetActiveEImage(false);
        }

        if (_playerNearbyChecker.IsPlayerNearby && Input.GetKey(KeyCode.E))
        {
            OnBrenchButtonClick();
        }
        else if(_playerNearbyChecker.IsPlayerNearby == false)
        {
            _viewer.gameObject.SetActive(false);
        }
    }

    public void OnBrenchButtonClick()
    {
        _buttonView.SetActiveEImage(false);
        _viewer.gameObject.SetActive(true);

        if (_isTutorialShowed == false)
        {
            _tutorial.ShowHowUpgrade();
            _isTutorialShowed = true;
        }
        
        if (_playerNearbyChecker.GetPlayer() != null)
        {
            _player = _playerNearbyChecker.GetPlayer();
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
