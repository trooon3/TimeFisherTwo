using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCountCatchedFishAd : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    
    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _bag.SetActiveIncrease();
    }
}
