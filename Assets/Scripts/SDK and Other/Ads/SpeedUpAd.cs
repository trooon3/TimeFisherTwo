using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpAd : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _mover.SetActiveIncrease();
    }
}
