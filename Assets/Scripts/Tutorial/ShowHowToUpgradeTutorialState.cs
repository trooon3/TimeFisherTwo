using Assets.Scripts.Tutorial;

public class ShowHowToUpgradeTutorialState : TutorialState
{
    public ShowHowToUpgradeTutorialState(TutorialViewer context) : base(context) { }

    public override void Enter()
    {
        Context.ShowHowUpgrade();
    }

    public override void Exit()
    {
        Context.HideHowUpgradeTutorial();
    }
}
