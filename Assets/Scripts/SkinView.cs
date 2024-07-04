using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Button _tryBuyButton;
    [SerializeField] private TMP_Text _tryBuyButtonText;
    [SerializeField] private Image _skinIcon;
    [SerializeField] private List<FishCountPriceView> _fishCosts;

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
        _tryBuyButtonText.text = "Купить";

        for (int i = 0; i < _fishCosts.Count; i++)
        {
            _fishCosts[i].Init(_cost.FishCountPrices[i].Icon, _cost.FishCountPrices[i].Cost.ToString());
        }
    }

    public void TrySetSkin()
    {
        if (!_isBuyed)
        {
            if (_closet.CheckCanPaySkin(this))
            {
                _isBuyed = true;
                _tryBuyButtonText.text = "Выбрать";
            }
        }
        else
        {
            _skinEditor.SetSkin(_skin);
        }
    }
}
