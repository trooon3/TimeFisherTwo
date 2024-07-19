using UnityEngine;
using UnityEngine.UI;

public class FieldOfCatchViewer : MonoBehaviour
{
    [SerializeField] private FishCatcher _field;
    [SerializeField] private Image _image;

    private void Update()
    {
        if (_field.FishToCatch != null)
        {
            _image.gameObject.SetActive(true);
        }
        else
        {
            _image.gameObject.SetActive(false);
        }
    }
}
