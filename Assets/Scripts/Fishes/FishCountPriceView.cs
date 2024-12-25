using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Fishes
{
    public class FishCountPriceView : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TMP_Text _cost;

        public void Init(Sprite sprite, string cost)
        {
            _sprite.sprite = sprite;
            _cost.text = cost;
        }
    }
}

