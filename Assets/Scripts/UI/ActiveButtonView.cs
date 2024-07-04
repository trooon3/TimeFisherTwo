using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonView : MonoBehaviour
{
    [SerializeField] private Image _eButtonImage;
    [SerializeField] private Button _eButton;

    public void SetActiveEImage(bool active)
    {
        _eButtonImage.gameObject.SetActive(active);
        _eButton.gameObject.SetActive(active);
    }
}
