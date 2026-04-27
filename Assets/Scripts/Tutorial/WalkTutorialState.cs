namespace Assets.Scripts.Tutorial
{
    public class WalkTutorialState : TutorialState
    {
        public WalkTutorialState(TutorialViewer context) : base(context) { }

        public override void Enter()
        {
            Context.ShowHowWalk();
        }

        public override void Exit()
        {
            Context.SetOffControlTutorial();
        }
    }
}