using UnityEngine;
using UnityEngine.UI;

public class SpeedUpAd : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Image _slider;
    [SerializeField] private AdTimeWorkView _adTimeWork;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _adTimeWork.StartShowAdTimeWork(_slider, _mover.IncreaseTimeSec);
        _mover.SetActiveIncrease();
    }
}
