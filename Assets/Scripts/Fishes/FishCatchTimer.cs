using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Fishes
{
    [RequireComponent(typeof(FishCatchTimerViewer))]
    public class FishCatchTimer : MonoBehaviour
    {
        private FishCatchTimerViewer _fishCatchTimerViewer;

        private void Start()
        {
            _fishCatchTimerViewer = GetComponent<FishCatchTimerViewer>();
        }

        public void StartChangeTimerValue()
        {
            _fishCatchTimerViewer.StartDisplayCatching();
        }

        public void ResetTime()
        {
            _fishCatchTimerViewer.ResetValue();
        }

        public void SetCatcher(FishCatcher catcher)
        {
            _fishCatchTimerViewer.SetCatcher(catcher);
        }

        public void ShowFillBag(bool active)
        {
            _fishCatchTimerViewer.ShowFilledBag(active);
        }
    }
}