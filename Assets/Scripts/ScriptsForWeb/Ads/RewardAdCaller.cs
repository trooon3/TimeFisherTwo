using YG;
using UnityEngine;

namespace Assets.Scripts.ScripsForWeb.Ads
{
    public class RewardAdCaller : MonoBehaviour
    {
        [SerializeField] private int _id;

        public void OpenRewardAd()
        {
            YandexGame.RewVideoShow(_id);
        }
    }
}

