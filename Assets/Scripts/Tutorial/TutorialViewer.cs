using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Saves;
using Assets.Scripts.UI;
using YG;

namespace Assets.Scripts.Tutorial
{
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

        [SerializeField] private SavesYG _savesYG;
        [SerializeField] private ButtonChangerController _buttonChangerController;

        private void Start()
        {
            if (!_savesYG.LoadTutorial(TutorialsKeys.IsShowTutorialWalk) || !_savesYG.LoadTutorial(TutorialsKeys.IsShowedCatchTutorial))
            {
                ShowHowWalk();
                ShowHowCatchFish();
                _buttonChangerController.SetButtonChangerOff();
            }
        }

        private void ShowHowWalk()
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                _howWalkMobile.SetActive(true);
            }
            else
            {
                _howWalk.SetActive(true);
            }

            _savesYG.SaveTutorial(TutorialsKeys.IsShowTutorialWalk, true);
        }

        private void ShowHowCatchFish()
        {
            _howCatchFishTuturial.SetActive(true);
            _savesYG.SaveTutorial(TutorialsKeys.IsShowedCatchTutorial, true);
        }

        public void ShowWhereFishesCollect()
        {
            if (!_savesYG.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
            {

                _arrowToCloset.gameObject.SetActive(true);
                _arrowToFishCount.gameObject.SetActive(false);
                _howGetFishToCloset.SetActive(true);
                _buttonChangerController.SetButtonChangerOff();
                Time.timeScale = 0;

                _savesYG.SaveTutorial(TutorialsKeys.IsShowedGetFishTutorial, true);
            }
        }

        public void ShowWhereFishesCount()
        {
            if (!_savesYG.LoadTutorial(TutorialsKeys.IsShowedGetFishTutorial))
            {
                _arrowToFishCount.gameObject.SetActive(true);
            }
        }

        public void ShowHowCatchFishOnRod()
        {
            _howCatchOnRodTutorial.SetActive(true);
            _buttonChangerController.SetButtonChangerOff();
            _arrowToCloset.gameObject.SetActive(false); 
            _savesYG.SaveTutorial(TutorialsKeys.IsShowTutorialRod, true);
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
            _buttonChangerController.SetButtonChangerOff();
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

