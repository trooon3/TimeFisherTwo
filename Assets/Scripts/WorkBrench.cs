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
    [SerializeField] private DataSaver _saver;

    private bool _isTutorialShowed;
    private string _tutorialShowedKey = "TutorialWorkBrenchKey";

    private void Awake()
    {
        var dtoTutorial = _saver.LoadTutorialData(_tutorialShowedKey);
        ApplySaves(dtoTutorial);
    }

    private void ApplySaves(DTOTutorial dtoTutorial)
    {
        if (dtoTutorial != null)
        {
            _isTutorialShowed = dtoTutorial.IsShowed;
        }
    }

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

        if(_playerNearbyChecker.IsPlayerNearby == false)
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
            _saver.SaveTutorialData(_tutorialShowedKey, new DTOTutorial { IsShowed = _isTutorialShowed });
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
