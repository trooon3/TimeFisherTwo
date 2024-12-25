using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MenuPauseController : MonoBehaviour
    {
        public void PauseTime()
        {
            Time.timeScale = 0f;
        }

        public void PlayTime()
        {
            Time.timeScale = 1f;
        }
    }
}

