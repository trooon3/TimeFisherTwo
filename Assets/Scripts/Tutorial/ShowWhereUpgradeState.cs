namespace Assets.Scripts.Tutorial
{
    public class ShowWhereUpgradeState : TutorialState
    {
        public ShowWhereUpgradeState(TutorialViewer context) : base(context) { }

        public override void Enter()
        {
            Context.ShowWhereUpgrade();
        }

        public override void Exit()
        {
            Context.HideUpgradeTutorial();
        }
    }
}