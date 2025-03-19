using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Saves;
using Assets.Scripts.UI;
using Assets.Scripts.Saves.DTO;
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

        [SerializeField] private DataSaver _saver;
        [SerializeField] private ButtonChangerController _buttonChangerController;

        private bool _isShowedWalkTutorial;
        private bool _isShowedCatchTutorial;
        private bool _isShowedGetFishTutorial;
        private readonly string _tutorialShowedKey = "DirectonGuideKey";

        private void Awake()
        {
            var dtoTutorial = _saver.LoadTutorialDirectonGuideData(_tutorialShowedKey);
            ApplySaves(dtoTutorial);
        }

        private void Start()
        {
            if (_isShowedCatchTutorial == false || _isShowedWalkTutorial == false)
            {
                ShowHowWalk();
                ShowHowCatchFish();
                _buttonChangerController.SetButtonChangerOff();
                DTODirectionGuide tutor = new DTODirectionGuide();
                tutor.Init(_isShowedGetFishTutorial, _isShowedCatchTutorial, _isShowedWalkTutorial);
                _saver.SaveTutorialDirectonGuideData(_tutorialShowedKey, tutor);
            }
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

            _isShowedWalkTutorial = true;
        }

        private void ShowHowCatchFish()
        {
            _howCatchFishTuturial.SetActive(true);
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
                DTODirectionGuide tutor = new DTODirectionGuide();
                tutor.Init(_isShowedGetFishTutorial, _isShowedCatchTutorial, _isShowedWalkTutorial);
                _saver.SaveTutorialDirectonGuideData(_tutorialShowedKey, tutor);
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
            _howCatchOnRodTutorial.SetActive(true);
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
            _howWalk.SetActive(false);
            _howWalkMobile.SetActive(false);
        }
    }
}

