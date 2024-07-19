using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesIncreaseAd : MonoBehaviour
{
    [SerializeField] private Closet _closet;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _closet.SetActiveIncrease();
    }
}
