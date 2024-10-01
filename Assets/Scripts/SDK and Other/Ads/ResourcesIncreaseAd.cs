using UnityEngine;
using UnityEngine.UI;

public class ResourcesIncreaseAd : MonoBehaviour
{
    [SerializeField] private Closet _closet;
    [SerializeField] private Image _slider;
    [SerializeField] private AdTimeWorkView _adTimeWork;

    public void OnClick()
    {
        var videoAd = new VideoAd();
        videoAd.Show(OnRevard);
    }

    private void OnRevard()
    {
        _adTimeWork.StartShowAdTimeWork(_slider, _closet.IncreaseTimeSec);
        _closet.SetActiveIncrease();
    }
}
