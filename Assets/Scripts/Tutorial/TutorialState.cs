using Assets.Scripts.Tutorial;
using UnityEngine;

public class TutorialState : MonoBehaviour
{
    protected TutorialViewer Context;
    protected TutorialState(TutorialViewer context) => Context = context;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
