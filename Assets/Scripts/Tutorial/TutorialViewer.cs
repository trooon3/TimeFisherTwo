using System;
using System.Collections.Generic;
using Assets.Scripts.FishResources;
using Assets.Scripts.Saves;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Assets.Scripts.Tutorial
{
    public class TutorialViewer : MonoBehaviour
    {
        [SerializeField] private Chest _closet;
        [SerializeField] private ResourcesManager _resourcesManager;
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

        [SerializeField] private ButtonChangerController _buttonChangerController;

        private Dictionary<Type, TutorialState> _states;
        private TutorialState _currentState;

        private void Start()
        {
            _states = new Dictionary<Type, TutorialState> {
            { typeof(WalkTutorialState), new WalkTutorialState(this) },
            { typeof(ShowHowCatchFishTutorialState), new ShowHowCatchFishTutorialState(this) },
            { typeof(ShowWhereFishesCountTutorialState), new ShowWhereFishesCountTutorialState(this) },
            { typeof(ShowWhereFishCollectTutorialState), new ShowWhereFishCollectTutorialState(this) },
            { typeof(ShowWhereUpgradeState), new ShowWhereUpgradeState(this) },
            { typeof(ShowHowToUpgradeTutorialState), new ShowWhereUpgradeState(this) } };

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialWalk) || !YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowedCatchTutorial))
            {
                ChangeState<WalkTutorialState>();
                ShowHowCatchFish();
                _buttonChangerController.SetButtonChangerOff();
            }
        }

        public void ChangeState<T>() where T : TutorialState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void ShowHowWalk()
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                _howWalkMobile.SetActive(true);
            }
            else
            {
                _howWalk.SetActive(true);
            }

            YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowTutorialWalk, true);
        }

        public void ShowHowCatchFish()
        {
            _howCatchFishTuturial.SetActive(true);
            YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowedCatchTutorial, true);
        }

        public void ShowWhereFishesCollect()
        {
            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
            {
                _arrowToCloset.gameObject.SetActive(true);
                _arrowToFishCount.gameObject.SetActive(false);
                _howGetFishToCloset.SetActive(true);
                _buttonChangerController.SetButtonChangerOff();
                Time.timeScale = 0;

                YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowedGetFishTutorial, true);
            }
        }

        public void HideWhereFishesCollect()
        {
            _arrowToCloset.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void ShowWhereFishesCount()
        {
            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
            {
                _arrowToFishCount.gameObject.SetActive(true);
            }
        }

        public void ShowHowCatchFishOnRod()
        {
            _howCatchOnRodTutorial.SetActive(true);
            _buttonChangerController.SetButtonChangerOff();
            _arrowToCloset.gameObject.SetActive(false);
            YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowTutorialRod, true);
        }

        public void ShowWhereUpgrade()
        {
            if (_bag.CountResourseToUpgrade <= _resourcesManager.GetFishBonesCount() || _rod.CountResourseToUpgrade <= _resourcesManager.GetFishBonesCount())
            {
                _arrowToWorkBranch.gameObject.SetActive(true);
                ShowWhereResources();
            }
        }

        public void HideUpgradeTutorial()
        {
            _arrowToWorkBranch.gameObject.SetActive(false);
            _arrowToResouces.gameObject.SetActive(false);
        }

        public void ShowHowUpgrade()
        {
            _howUpgrade.gameObject.SetActive(true);
            _buttonChangerController.SetButtonChangerOff();
        }

        public void HideHowUpgradeTutorial()
        {
            _howUpgrade.gameObject.SetActive(false);
        }

        public void ShowWhereResources()
        {
            _arrowToResouces.gameObject.SetActive(true);
        }

        public void SetOffControlTutorial()
        {
            _howWalk.SetActive(false);
            _howWalkMobile.SetActive(false);
        }
    }
}

