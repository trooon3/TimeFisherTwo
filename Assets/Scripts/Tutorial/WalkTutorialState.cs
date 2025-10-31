using Assets.Scripts.Saves;
using Assets.Scripts.Tutorial;
using UnityEngine;
using YG;

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
