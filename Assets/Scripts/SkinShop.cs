using Assets.Scripts.Fishes;
using Assets.Scripts.SkinScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkinShop : MonoBehaviour
    {
        [SerializeField] private SkinView _template;
        [SerializeField] private Transform _container;
        [SerializeField] private SkinEditor _skinEditor;
        [SerializeField] private FishesManager _fishesManager;

        private List<SkinView> _views;

        private void Start()
        {
            _views = new List<SkinView>();

            foreach (var item in _skinEditor.Skins)
            {
                var skinview = Instantiate(_template, _container);

                skinview.Init(_skinEditor, item, this);
                skinview.gameObject.SetActive(true);

                _views.Add(skinview);
            }
        }

        private void PaySkin(SkinView skin)
        {
            SkinCost cost = skin.Cost;

            foreach (var fishPrice in cost.FishCountPrices)
            {
                for (int i = 0; i < fishPrice.Cost; i++)
                {
                    _fishesManager.RemoveFish(fishPrice.Type);
                }
            }
        }

        public bool CheckCanPaySkin(SkinView skin)
        {
            SkinCost cost = skin.Cost;

            foreach (var fishPrice in cost.FishCountPrices)
            {
                foreach (var fish in _fishesManager.CatchedFishes)
                {
                    if (fishPrice.Type == fish.Type)
                    {
                        if (fishPrice.Cost > fish.Count)
                        {
                            return false;
                        }
                    }
                }
            }

            PaySkin(skin);
            return true;
        }
    }
}

