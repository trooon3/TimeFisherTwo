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

        private SkinShop _skinShop;
        private SkinNames _nameKey;
        private SkinEditor _skinEditor;
        private Skin _skin;
        private SkinCost _cost;

        public SkinCost Cost => _cost;

        public void Init(SkinEditor editor, Skin forSkin, SkinShop skinShop)
        {
            _skinShop = skinShop;
            _skinEditor = editor;
            _skin = forSkin;
            _skinIcon.sprite = forSkin.Icon;
            _cost = forSkin.Cost;
            _cost.SetListPrices();
            _nameKey = forSkin.Name;

            YandexGame.savesData.LoadSkinsSaves(_nameKey);

            if (YandexGame.savesData.LoadSkinsSaves(_nameKey))
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

        public void TrySetSkin()
        {
            if (!YandexGame.savesData.LoadSkinsSaves(_nameKey))
            {
                if (_skinShop.CheckCanPaySkin(this))
                {
                    _tryBuyButtonText.text = Lean.Localization.LeanLocalization.GetTranslationText("Buyed");
                    YandexGame.savesData.SaveSkins(_nameKey, true);
                }
            }
            else
            {
                _skinEditor.SetSkin(_skin);
            }
        }
    }
}