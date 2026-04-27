using Assets.Scripts.ScripsForWeb.Ads;
using UnityEngine;

namespace Assets.Scripts.SkinScripts
{
    public class ChestAdManager : MonoBehaviour
    {
        [SerializeField] private InterAd _interAd;

        public void ShowAd()
        {
            _interAd.ShowAd();
        }
    }
}
