using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodSpeedUpAd : MonoBehaviour
{
    [SerializeField] private Rod _rod;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _rod.SetActiveIncrease();
    }
}
