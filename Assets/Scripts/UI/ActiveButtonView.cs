using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ActiveButtonView : MonoBehaviour
    {
        [SerializeField] private Image _eButtonImage;
        [SerializeField] private Button _eButton;

        public void SetActiveEImage(bool active)
        {
            _eButton.gameObject.SetActive(active);
        }
    }
}

