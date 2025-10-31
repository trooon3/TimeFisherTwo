using Assets.Scripts.FishResources;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Saves;
using Assets.Scripts.ScripsForWeb.Ads;
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
        [SerializeField] private Chest _closet;
        [SerializeField] private ResourcesManager _resourcesManager;
        [SerializeField] private List<GameObject> _tools;
        [SerializeField] private WorkBranchViewer _viewer;
        [SerializeField] private InterAd _interAd;
        [SerializeField] private ActiveButtonView _buttonView;
        [SerializeField] private TutorialViewer _tutorial;

        private void Awake()
        {
            YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialWorkBranch);
            OnPlayerApproach(false);
        }

        private bool CheckIsCanPay(Resource resource, int count)
        {
            foreach (var resourceType in _resourcesManager.ResCounters)
            {
                if (resourceType.Resource == resource)
                {
                    if (resourceType.Count >= count)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnPlayerApproach(bool isPlayerApproach)
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

        public void TryUpgrade(Equipment tool)
        {
            var needResource = tool.GetResourceToUpgrade();
            var needCountResource = tool.GetResourceCountToUpgrade();

            if (CheckIsCanPay(needResource, needCountResource))
            {
                _resourcesManager.SpendResources(needCountResource, needResource);
                tool.Upgrade();
                tool.CheckLevel();
            }
        }

        public void OnBrenchButtonClick()
        {
            _interAd.ShowAd();
            _buttonView.SetActiveEImage(false);
            _viewer.gameObject.SetActive(true);

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialWorkBranch))
            {
                _tutorial.ChangeState<ShowHowToUpgradeTutorialState>();
                YandexGame.savesData.SaveTutorial(TutorialsKeys.IsShowTutorialWorkBranch, true);
            }
        }
    }
}