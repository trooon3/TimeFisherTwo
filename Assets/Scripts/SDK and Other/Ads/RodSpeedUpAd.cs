using UnityEngine;
using UnityEngine.UI;

public class RodSpeedUpAd : MonoBehaviour
{
    [SerializeField] private Rod _rod;
    [SerializeField] private Image _slider;
    [SerializeField] private AdTimeWorkView _adTimeWork;

    private void Start()
    {
        _slider.fillAmount = 0;
    }

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _adTimeWork.StartShowAdTimeWork(_slider, _rod.IncreaseTimeSec);
        _rod.SetActiveIncrease();
    }
}
