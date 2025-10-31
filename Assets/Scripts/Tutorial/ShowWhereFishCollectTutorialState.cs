using Assets.Scripts.Saves;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Assets.Scripts.Tutorial
{
    internal class ShowWhereFishCollectTutorialState : TutorialState
    {
        public ShowWhereFishCollectTutorialState(TutorialViewer context) : base(context) { }

        public override void Enter()
        {
            Context.ShowWhereFishesCollect();
        }

        public override void Exit()
        {
            Context.HideWhereFishesCollect();
        }
    }
}