using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.Fishes;
using YG;
using Assets.Scripts.Saves;

namespace Assets.Scripts.SkinScripts
{
    public class SkinView : MonoBehaviour
    {
        [SerializeField] private Button _tryBuyButton;
        [SerializeField] private TMP_Text _tryBuyButtonText;
        [SerializeField] private Image _skinIcon;
        [SerializeField] private List<FishCountPriceView> _fishCosts;
        [SerializeField] private SavesYG _savesYG;

        private SkinNames _nameKey;
        private Closet _closet;
        private SkinEditor _skinEditor;
        private Skin _skin;
        private SkinCost _cost;

        public SkinCost Cost => _cost;

        public void Init(SkinEditor editor, Skin forSkin, Closet closet)
        {
            _closet = closet;
            _skinEditor = editor;
            _skin = forSkin;
            _skinIcon.sprite = forSkin.Icon;
            _cost = forSkin.Cost;
            _cost.SetListPrices();
            _nameKey = forSkin.Name;

            _savesYG.LoadSkinsSaves(_nameKey);

            if (_savesYG.LoadSkinsSaves(_nameKey))
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

        public void SetName(SkinNames name)
        {
            _nameKey = name;
        }

        public void TrySetSkin()
        {
            if (!_savesYG.LoadSkinsSaves(_nameKey))
            {
                if (_closet.CheckCanPaySkin(this))
                {
                    _tryBuyButtonText.text = Lean.Localization.LeanLocalization.GetTranslationText("Buyed");
                    _savesYG.SaveSkins(_nameKey, true);
                }
            }
            else
            {
                _skinEditor.SetSkin(_skin);
            }
        }
    }
}

