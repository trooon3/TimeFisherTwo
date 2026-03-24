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