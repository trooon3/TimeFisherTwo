using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialViewer : MonoBehaviour
{
    [SerializeField] private Closet _closet;
    [SerializeField] private Rod _rod;
    [SerializeField] private Bag _bag;

    [SerializeField] private Image _arrowToCloset;
    [SerializeField] private Image _arrowToWorkBranch;
    [SerializeField] private Image _arrowToResouces;
    [SerializeField] private Image _arrowToFishCount;
    [SerializeField] private Image _arrowToShop;
    [SerializeField] private GameObject _howGetFishToCloset;
    [SerializeField] private GameObject _howUpgrade;
    [SerializeField] private GameObject _howCatchOnRodTutorial;
    [SerializeField] private GameObject _howCatchFishTuturial;
    [SerializeField] private GameObject _howWalk;
    [SerializeField] private GameObject _howWalkMobile;

    private void Start()
    {
        ShowHowWalk();
        ShowHowCatchFish();
    }

    private void ShowHowWalk()
    {
        _howWalk.gameObject.SetActive(true);
    }

    private void ShowHowCatchFish()
    {
        _howCatchFishTuturial.gameObject.SetActive(true);
    }

    public void ShowWhereFishesCollect()
    {
        _arrowToCloset.gameObject.SetActive(true);
        _arrowToFishCount.gameObject.SetActive(false);
        _howGetFishToCloset.SetActive(true);
    }

    public void ShowWhereFishesCount()
    {
        _arrowToFishCount.gameObject.SetActive(true);
    }

    public void ShowHowCatchFishOnRod()
    {
        _howCatchOnRodTutorial.gameObject.SetActive(true);
        _arrowToCloset.gameObject.SetActive(false);
    }

    public void ShowWhereUpgrade()
    {
        if (_bag.CountResourseToUpgrade <= _closet.GetFishBonesCount() || _rod.CountResourseToUpgrade <= _closet.GetFishBonesCount())
        {
            _arrowToWorkBranch.gameObject.SetActive(true);
            ShowWhereResources();
        }
    }

    public void ShowHowUpgrade()
    {
        _howUpgrade.gameObject.SetActive(true);
    }

    public void ShowWhereResources()
    {
        _arrowToResouces.gameObject.SetActive(true);
    }

    public void ShowWhereShop()
    {
        _arrowToShop.gameObject.SetActive(true);
    }

    public void SetOffControlTutorial()
    {
        _howWalk.gameObject.SetActive(false);
        _howWalkMobile.gameObject.SetActive(false);
    }
}
