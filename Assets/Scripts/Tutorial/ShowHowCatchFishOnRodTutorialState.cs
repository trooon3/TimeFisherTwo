using Assets.Scripts.Tutorial;

internal class ShowHowCatchFishOnRodTutorialState : TutorialState
{
    public ShowHowCatchFishOnRodTutorialState(TutorialViewer context) : base(context) { }

    public override void Enter()
    {
        Context.ShowHowCatchFishOnRod();
    }
}
