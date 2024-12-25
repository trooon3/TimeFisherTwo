using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SkinScripts
{
    [CreateAssetMenu(fileName = "ShopAtribut", menuName = "ShopAtributs/SkinCost")]
    public class SkinCost : ScriptableObject
    {
        private List<FishCountPrice> _fishCountPrices;
        [SerializeField] private FishCountPrice _fourthFishTypeCost;
        [SerializeField] private FishCountPrice _firstFishTypeCost;
        [SerializeField] private FishCountPrice _secondFishTypeCost;
        [SerializeField] private FishCountPrice _thirdFishTypeCost;

        public List<FishCountPrice> FishCountPrices => _fishCountPrices;
        public FishCountPrice FIshCountPrice => _firstFishTypeCost;
        public FishCountPrice SecondFishTypeCost => _secondFishTypeCost;
        public FishCountPrice ThirdFishTypeCost => _thirdFishTypeCost;
        public FishCountPrice FourthFishTypeCost => _fourthFishTypeCost;

        public void SetListPrices()
        {
            _fishCountPrices = new List<FishCountPrice>();
            _fishCountPrices.Add(_firstFishTypeCost);
            _fishCountPrices.Add(_secondFishTypeCost);
            _fishCountPrices.Add(_thirdFishTypeCost);
            _fishCountPrices.Add(_fourthFishTypeCost);
        }
    }
}

