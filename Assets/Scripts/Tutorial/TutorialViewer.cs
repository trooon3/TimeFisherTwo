using UnityEngine;
using UnityEngine.UI;
using Agava.WebUtility;

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
        [SerializeField] private DataSaver _saver;
        [SerializeField] private ButtonChangerController _buttonChangerController;

        private bool _isShowedWalkTutorial;
        private bool _isShowedCatchTutorial;
        private bool _isShowedGetFishTutorial;
        private string _tutorialShowedKey = "DirectonGuideKey";

        private void Awake()
        {
            var dtoTutorial = _saver.LoadTutorialDirectonGuideData(_tutorialShowedKey);
            ApplySaves(dtoTutorial);
        }

        private void ApplySaves(DTODirectionGuide dtoTutorial)
        {
            if (dtoTutorial != null)
            {
                _isShowedWalkTutorial = dtoTutorial.IsShowedWalkTutorial;
                _isShowedCatchTutorial = dtoTutorial.IsShowedCatchTutorial;
                _isShowedGetFishTutorial = dtoTutorial.IsShowedGetFishTutorial;
            }
        }

        private void Start()
        {
            if (_isShowedCatchTutorial == false || _isShowedWalkTutorial == false)
            {
                ShowHowWalk();
                ShowHowCatchFish();
                _buttonChangerController.SetButtonChangerOff();
                _saver.SaveTutorialDirectonGuideData(_tutorialShowedKey, new DTODirectionGuide
                {
                    IsShowedGetFishTutorial = _isShowedGetFishTutorial,
                    IsShowedCatchTutorial = _isShowedCatchTutorial,
                    IsShowedWalkTutorial = _isShowedWalkTutorial
                });
            }
        }

        private void ShowHowWalk()
        {
            if (Device.IsMobile)
            {
                _howWalkMobile.gameObject.SetActive(true);
            }
            else
            {
                _howWalk.gameObject.SetActive(true);
            }

            _isShowedWalkTutorial = true;
        }

        private void ShowHowCatchFish()
        {
            _howCatchFishTuturial.gameObject.SetActive(true);
            _isShowedCatchTutorial = true;
        }

        public void ShowWhereFishesCollect()
        {
            if (_isShowedGetFishTutorial == false)
            {

                _arrowToCloset.gameObject.SetActive(true);
                _arrowToFishCount.gameObject.SetActive(false);
                _howGetFishToCloset.SetActive(true);
                _buttonChangerController.SetButtonChangerOff();
                Time.timeScale = 0;

                _isShowedGetFishTutorial = true;
                _saver.SaveTutorialDirectonGuideData(_tutorialShowedKey, new DTODirectionGuide
                {
                    IsShowedGetFishTutorial = _isShowedGetFishTutorial,
                    IsShowedCatchTutorial = _isShowedCatchTutorial,
                    IsShowedWalkTutorial = _isShowedWalkTutorial
                });
            }
        }

        public void ShowWhereFishesCount()
        {
            if (_isShowedGetFishTutorial == false)
            {
                _arrowToFishCount.gameObject.SetActive(true);
            }
        }

        public void ShowHowCatchFishOnRod()
        {
            _howCatchOnRodTutorial.gameObject.SetActive(true);
            _buttonChangerController.SetButtonChangerOff();
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
            _buttonChangerController.SetButtonChangerOff();
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
}

