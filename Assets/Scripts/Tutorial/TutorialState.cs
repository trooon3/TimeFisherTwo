using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    public class TutorialState : MonoBehaviour
    {
        protected TutorialViewer Context;
        protected TutorialState(TutorialViewer context) => Context = context;

        public virtual void Enter() { }
        public virtual void Exit() { }
    }
}