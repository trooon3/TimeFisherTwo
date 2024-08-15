using Agava.YandexGames;
using Agava.WebUtility;
using UnityEngine;

public class DeviceChooser : MonoBehaviour
{
    private string _isMobileText;
    private void Start()
    {
        _isMobileText = $"{nameof(Device)}.{nameof(Device.IsMobile)} = {Device.IsMobile}";
        Debug.Log(_isMobileText);
        Debug.Log(_isMobileText);
        Debug.Log(_isMobileText);
        Debug.Log(_isMobileText);
    }
}
