using YG;
using UnityEngine;

public class RewardAdCaller : MonoBehaviour
{
    [SerializeField] private int _id;

    public void OpenRewardAd()
    {
        YandexGame.RewVideoShow(_id);
    }
}
