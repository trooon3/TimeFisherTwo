namespace Assets.Scripts.Tutorial
{
    internal class ShowWhereFishesCountTutorialState : TutorialState
    {
        public ShowWhereFishesCountTutorialState(TutorialViewer context) : base(context) { }

        public override void Enter()
        {
            Context.ShowWhereFishesCount();
        }
    }
}