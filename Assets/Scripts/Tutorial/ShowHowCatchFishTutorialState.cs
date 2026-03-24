namespace Assets.Scripts.Tutorial
{
    internal class ShowHowCatchFishTutorialState : TutorialState
    {
        public ShowHowCatchFishTutorialState(TutorialViewer context) : base(context) { }

        public override void Enter()
        {
            Context.ShowHowCatchFish();
        }
    }
}