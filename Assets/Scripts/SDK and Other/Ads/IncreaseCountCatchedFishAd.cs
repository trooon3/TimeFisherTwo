using UnityEngine;
using UnityEngine.UI;

public class IncreaseCountCatchedFishAd : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private Image _slider;
    [SerializeField] private AdTimeWorkView _adTimeWork;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _adTimeWork.StartShowAdTimeWork(_slider, _bag.IncreaseTimeSec);
        _bag.SetActiveIncrease();
    }
}
