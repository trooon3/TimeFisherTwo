using UnityEngine;
using TMPro;
using Assets.Scripts.Tutorial;

namespace Assets.Scripts.UI
{
    public class BagCountViewer : MonoBehaviour
    {
        [SerializeField] private FishStorage _bag;
        [SerializeField] private TutorialViewer _tutorial;

        [SerializeField] private TMP_Text _fishCount;
        [SerializeField] private TMP_Text _filledBagText;

        private bool _isTutorialShowed;

        private void Start()
        {
            OnFishAdded();
            OnBagEmpty();
        }

        private void OnEnable()
        {
            _bag.FishCountChanged += OnFishAdded;
            _bag.BagFilled += OnBagFilled;
            _bag.BagDevastated += OnBagEmpty;
        }

        private void OnDisable()
        {
            _bag.FishCountChanged -= OnFishAdded;
            _bag.BagFilled -= OnBagFilled;
            _bag.BagDevastated -= OnBagEmpty;
        }

        private void OnFishAdded()
        {
            _fishCount.text = _bag.CurrentCount.ToString();
        }

        private void OnBagFilled()
        {
            if (!_isTutorialShowed)
            {
                _tutorial.ChangeState<ShowWhereFishCollectTutorialState>();
                _isTutorialShowed = true;
            }

            _filledBagText.gameObject.SetActive(true);
        }

        private void OnBagEmpty()
        {
            _filledBagText.gameObject.SetActive(false);
        }
    }
}

