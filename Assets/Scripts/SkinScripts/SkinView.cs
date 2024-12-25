using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Assets.Scripts.SkinScripts
{
    public class SkinView : MonoBehaviour
    {
        [SerializeField] private Button _tryBuyButton;
        [SerializeField] private TMP_Text _tryBuyButtonText;
        [SerializeField] private Image _skinIcon;
        [SerializeField] private List<FishCountPriceView> _fishCosts;
        [SerializeField] private DataSaver _saver;

        private string _nameKey;
        private Closet _closet;
        private SkinEditor _skinEditor;
        private Skin _skin;
        private SkinCost _cost;

        private bool _isBuyed;

        public SkinCost Cost => _cost;
        public Skin Skin => _skin;

        public void Init(SkinEditor editor, Skin forSkin, Closet closet)
        {
            _closet = closet;
            _skinEditor = editor;
            _skin = forSkin;
            _skinIcon.sprite = forSkin.Icon;
            _cost = forSkin.Cost;
            _cost.SetListPrices();
            _nameKey = forSkin.Name;

            var dtoSkin = _saver.LoadSkinData(_nameKey);

            ApplySaves(dtoSkin);

            if (_isBuyed)
            {
                _tryBuyButtonText.text = Lean.Localization.LeanLocalization.GetTranslationText("Buyed");
            }
            else
            {
                _tryBuyButtonText.text = Lean.Localization.LeanLocalization.GetTranslationText("NotBuyed");
            }

            for (int i = 0; i < _fishCosts.Count; i++)
            {
                _fishCosts[i].Init(_cost.FishCountPrices[i].Icon, _cost.FishCountPrices[i].Cost.ToString());
            }
        }

        public void SetName(string name)
        {
            _nameKey = name;
        }

        private void ApplySaves(DTOSkin dTOSkin)
        {
            if (dTOSkin != null)
            {
                _isBuyed = dTOSkin.Isbuyed;
            }
        }

        public void TrySetSkin()
        {
            if (!_isBuyed)
            {
                if (_closet.CheckCanPaySkin(this))
                {
                    _isBuyed = true;
                    _tryBuyButtonText.text = Lean.Localization.LeanLocalization.GetTranslationText("Buyed");

                    _saver.SaveSkinData(_nameKey, new DTOSkin { Isbuyed = _isBuyed, Name = _nameKey });
                }
            }
            else
            {
                _skinEditor.SetSkin(_skin);
            }
        }
    }
}

