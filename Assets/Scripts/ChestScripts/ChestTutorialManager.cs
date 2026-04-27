using Assets.Scripts.Saves;
using Assets.Scripts.Tutorial;
using Assets.Scripts.UI;
using UnityEngine;
using YG;

namespace Assets.Scripts.SkinScripts
{
    public class ChestTutorialManager : MonoBehaviour
    {
        [SerializeField] private TutorialViewer _tutorial;
        [SerializeField] private RodCatchViewer _rodView;

        public void HandleChestInteraction()
        {
            _rodView.SetOffHappyFace();

            if (!YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialRod))
            {
                _tutorial.ChangeState<ShowHowCatchFishOnRodTutorialState>();
            }
            else
            {
                _tutorial.ChangeState<ShowWhereUpgradeState>();
            }
        }
    }
}
