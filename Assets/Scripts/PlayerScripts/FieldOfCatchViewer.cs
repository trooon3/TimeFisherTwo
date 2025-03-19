using Assets.Scripts.Fishes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerScripts
{
    public class FieldOfCatchViewer : MonoBehaviour
    {
        [SerializeField] private FishCatcher _field;
        [SerializeField] private Image _image;

        private void Start()
        {
            _image.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _field.FishFinded += ImageViewControl;
        }

        private void OnDisable()
        {
            _field.FishFinded -= ImageViewControl;
        }

        private void ImageViewControl(Fish fish)
        {
            if (fish != null)
            {
                _image.gameObject.SetActive(true);
            }
            else
            {
                _image.gameObject.SetActive(false);
            }
        }
    }
}

