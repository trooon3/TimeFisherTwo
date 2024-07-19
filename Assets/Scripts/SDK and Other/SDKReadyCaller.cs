using Agava.YandexGames;
using UnityEngine;

public class SDKReadyCaller : MonoBehaviour
{
    void Awake()
    {
        YandexGamesSdk.GameReady();
    }
}

