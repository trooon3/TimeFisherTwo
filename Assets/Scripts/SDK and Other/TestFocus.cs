using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFocus : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackGrondChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackGrondChangeWeb;

    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackGrondChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackGrondChangeWeb;
    }

    private void OnInBackGrondChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackGrondChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        _audioSource.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
