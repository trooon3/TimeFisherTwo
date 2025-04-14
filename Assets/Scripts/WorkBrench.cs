using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Saves;
using Assets.Scripts.Tutorial;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class WorkBrench : MonoBehaviour
    {
        [SerializeField] private PlayerNearbyChecker _playerNearbyChecker;
        [SerializeField] private Closet _closet;
        [SerializeField] private List<GameObject> _tools;
        [SerializeField] private WorkBranchViewer _viewer;
        [SerializeField] private ActiveButtonView _buttonView;
        [SerializeField] private TutorialViewer _tutorial;

        private void Awake()
        {
            YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialWorkBranch);
            OnPlayerApproach(false);
        }

        private void OnEnable()
        {
            _playerNearbyChecker.PlayerNearby += OnPlayerApproach;
        }

        private void OnDisable()
        {
            _playerNearbyChecker.PlayerNearby -= OnPlayerApproach;
        }

        public void OnBrenchButtonClick()
        {
            _buttonView.SetActiveEImage(false);
            _viewer.gameObject.SetActive(true);

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialWorkBranch))
            {
                _tutorial.ShowHowUpgrade();
                YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowTutorialWorkBranch, true);
            }
        }

        public void TryUpgrade(Equipment tool)
        {
            var needResource = tool.GetResourceToUpgrade();
            var needCountResource = tool.GetResourceCountToUpgrade();

            if (_closet.CheckIsCanPay(needResource, needCountResource))
            {
                _closet.SpendResources(needCountResource, needResource);
                tool.Upgrade();
                tool.CheckLevel();
            }
        }

        private void OnPlayerApproach(bool isPlayerApproach)
        {
            if (isPlayerApproach)
            {
                _buttonView.SetActiveEImage(true);
            }
            else
            {
                _viewer.gameObject.SetActive(false);
                _buttonView.SetActiveEImage(false);
            }
        }
    }
}

