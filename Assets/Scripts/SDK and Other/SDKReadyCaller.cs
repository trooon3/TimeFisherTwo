using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKReadyCaller : MonoBehaviour
{
    void Awake()
    {
        YandexGamesSdk.GameReady();
    }
}

