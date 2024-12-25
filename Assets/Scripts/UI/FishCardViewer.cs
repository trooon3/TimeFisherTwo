using Assets.Scripts.Fishes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FishCardViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _hook;

        private ClosetView _closet;
        private SeaCreature _seaCreature;
        private FishTypeCounter _counter;
        public SeaCreature SeaCreature => _seaCreature;

        public void Init(SeaCreature fish)
        {
            _seaCreature = fish;
            _label.text = fish.Name;
            _icon.sprite = fish.Icon;
        }

        public void SetCloset(ClosetView closet)
        {
            _closet = closet;
        }

        public void SetCounter(FishTypeCounter counter)
        {
            _counter = counter;
            RefreshCount();
        }

        public void RefreshCount()
        {
            _count.text = _counter.Count.ToString();
        }

        public int GetCount()
        {
            return _counter.Count;
        }

        public void SetActiveHookButton(bool active)
        {
            _hook.gameObject.SetActive(active);
        }

        public void Hook()
        {
            if (_counter.Count <= 0)
            {
                _hook.enabled = false;
            }

            if (_hook.enabled)
            {
                _closet.OnHookButtonClick(_seaCreature.FishType);
            }
        }
    }
}

