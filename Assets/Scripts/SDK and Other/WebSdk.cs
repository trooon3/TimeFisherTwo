using UnityEngine;
using System;
using Agava.YandexGames;
using System.Collections;
using UnityEngine.SceneManagement;
using Agava.WebUtility;

public class WebSdk : MonoBehaviour
{
    private const string LeaderBoardName = "Leaderboard";

    [SerializeField] private bool _isStartInterstitialAD = true;

    public event Action<string> RewardVideoStarted;
    public event Action<string> RewardVideoCompleted;
    public event Action<string> RewardVideoClosed;
    public event Action<string> RewardVideoErrored;
    public event Action InterstitalVideoStart;
    public event Action InterstitalVideoClosed;
    public event Action InterstitalVideoErrored;
    public event Action InterstitalVideoOffline;
    public event Action FriendsInvited;
    public event Action Initialized;
    public event Action PersonalPermissionGeted;
    public event Action AdBlockDetected;
    public event Action<LeaderboardGetEntriesResponse> LeaderboardGeted;

    public static event Action<bool> ADPlayed;

    private int _lastScore;
    private string _requestID;
    private VideoType _videoType = VideoType.Not;

    private bool isRewardOpend = false;


#if YANDEX_GAMES && UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();

        if (_isStartInterstitialAD == true)
            ShowInterstitialAD();
        Initialized?.Invoke();
    }
#endif


#if CRAZY_GAMES ||GAME_DISTRIBUTION && UNITY_WEBGL && !UNITY_EDITOR
    private void Start()
    {
        //Debug.Log("WebSDK Start ShowInterstitialAD()");
        //ShowInterstitialAD();
    }
#endif

    public void ShowRewardedAD(string rewardID, bool detectADBlock)
    {
        //Debug.Log("ShowRewardedAD()");

        if (isRewardOpend == true)
        {
            return;
        }

        isRewardOpend = true;

        _requestID = rewardID;
#if YANDEX_GAMES
        if (AdBlock.Enabled == true)
        {
            if (detectADBlock == true)
                AdBlockDetected?.Invoke();

            CompletePause();
            RewardVideoErrored?.Invoke(_requestID);
            return;
        }
        Agava.YandexGames.VideoAd.Show(OnRewardedOpenCallback, OnRewardedCompleteCallback, OnRewardedCloseCallback, OnRewardedErrorCallback);
        RewardVideoStarted?.Invoke(_requestID);
#endif
#if CRAZY_GAMES && UNITY_WEBGL && !UNITY_EDITOR
        RewardVideoStarted?.Invoke(_requestID);
        CrazyAds.Instance.beginAdBreakRewarded(OnVideoStartedCallback, OnRewardedCompleteCallback, OnRewardedErrorCallback);
#endif

#if VK_GAMES
        BeginPause();
        RewardVideoStarted?.Invoke(_requestID);
        Agava.VKGames.VideoAd.Show(OnRewardedCallback, OnErrorCallback);
#endif
#if GAME_DISTRIBUTION
        _videoType = VideoType.Rewarded;
        RewardVideoStarted?.Invoke(_requestID);
        GameDistribution.Instance.ShowRewardedAd();
#endif
    }

    public void ShowInterstitialAD()
    {
#if YANDEX_GAMES && UNITY_WEBGL && !UNITY_EDITOR
        if (AdBlock.Enabled == true)
        {
            CompletePause();
            return;
        }
        InterstitialAd.Show(OnInterstitialOpenCallback, OnInterstitialCloseCallback, OnInterstitialErrorCallback, OnInterstitialOfflineCallback);
#endif
#if VK_GAMES
        VideoAd.Show(OnRewardedCallback, OnErrorCallback);
#endif
#if CRAZY_GAMES && UNITY_WEBGL && !UNITY_EDITOR
        CrazyAds.Instance.beginAdBreak(OnVideoStartedCallback, OnInterstitialCompleted, OnInterstitialErrored);
#endif
#if GAME_DISTRIBUTION
        Debug.Log("ShowInterstitialAD()");

        _videoType = VideoType.Interstital;
        GameDistribution.Instance.ShowAd();
#endif
    }

#if YANDEX_GAMES
    #region YANDEXAuthorizePlayer
    public void AuthorizePlayer()
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize(TryGetPersonalProfileDataPermission);
    }

    public void TryGetPersonalProfileDataPermission()
    {
        if (PlayerAccount.IsAuthorized == true && PlayerAccount.HasPersonalProfileDataPermission == false)
        {
            PlayerAccount.RequestPersonalProfileDataPermission(OnPersonalDataPermission);
        }
    }

    private void OnPersonalDataPermission()
    {
        PersonalPermissionGeted?.Invoke();
    }
    #endregion

    #region YANDEXInterstitialAD
    private void OnInterstitialOpenCallback()
    {
        InterstitalVideoStart?.Invoke();
        BeginPause();
    }

    private void OnInterstitialCloseCallback(bool obj)
    {
        InterstitalVideoClosed?.Invoke();
        CompletePause();
    }

    private void OnInterstitialErrorCallback(string obj)
    {
        InterstitalVideoErrored?.Invoke();
        CompletePause();
    }

    private void OnInterstitialOfflineCallback()
    {
        InterstitalVideoOffline?.Invoke();
    }
    #endregion


    #region YANDEXRewardedAD
    private void OnRewardedOpenCallback() => BeginPause();


    private void OnRewardedCompleteCallback()
    {
        RewardVideoCompleted?.Invoke(_requestID);
    }

    private void OnRewardedCloseCallback()
    {
        CompletePause();
        RewardVideoClosed?.Invoke(_requestID);
    }

    private void OnRewardedErrorCallback(string obj)
    {
        CompletePause();
        RewardVideoErrored?.Invoke(_requestID);
    }
    #endregion
#endif

    private void BeginPause()
    {
        ADPlayed?.Invoke(true);
        Debug.LogWarning("LEV_BeginPause");
        //InterstitalVideoStart?.Invoke();
    }

    private void CompletePause()
    {
        isRewardOpend = false;
        Debug.LogWarning("LEV_CompletePause");
        ADPlayed?.Invoke(false);
    }

#if VK_GAMES && UNITY_WEBGL && !UNITY_EDITOR
    private IEnumerator Start()
    {
        yield return VKGamesSdk.Initialize();
        BeginPause();
        Interstitial.Show(OnInterstitialOpenCallback, OnInterstitialErrorCallback);
    }

    public void SetLeaderboardScore(int score)
    {
        Leaderboard.ShowLeaderboard(score);
    }

    public void InviteFriends()
    {
        SocialInteraction.InviteFriends(OnFriendsInvited);
    }

    private void OnFriendsInvited() => FriendsInvited?.Invoke();   

    private void OnInterstitialOpenCallback() => CompletePause();

    private void OnInterstitialErrorCallback() => CompletePause();

    private void OnRewardedCallback()
    {
        CompletePause();
        RewardVideoCompleted?.Invoke(_requestID);
    }

    private void OnErrorCallback() => CompletePause();
#endif

#if CRAZY_GAMES && UNITY_WEBGL && !UNITY_EDITOR
    private void OnVideoStartedCallback()
    {
        BeginPause();
    }

    private void OnRewardedCompleteCallback()
    {
        CompletePause();
        RewardVideoCompleted?.Invoke(_requestID);
        RewardVideoClosed?.Invoke(_requestID);
    }
    private void OnRewardedErrorCallback()
    {
        CompletePause();
        RewardVideoErrored?.Invoke(_requestID);
    }

    private void OnInterstitialCompleted()
    {
        CompletePause();
        InterstitalVideoCompleted?.Invoke();
    }

    private void OnInterstitialErrored()
    {
        CompletePause();
        InterstitalVideoErrored?.Invoke();
    }
#endif
#if GAME_DISTRIBUTION

    private void OnEnable()
    {
        GameDistribution.OnResumeGame += OnResumeGame;
        GameDistribution.OnPauseGame += OnPauseGame;
        GameDistribution.OnRewardGame += OnRewardedGame;
        GameDistribution.OnRewardedVideoFailure += OnRewardedVideoFailure;
    }

    private void OnDisable()
    {
        GameDistribution.OnResumeGame -= OnResumeGame;
        GameDistribution.OnPauseGame -= OnPauseGame;
        GameDistribution.OnRewardGame -= OnRewardedGame;
        GameDistribution.OnRewardedVideoFailure -= OnRewardedVideoFailure;
    }

    private void OnRewardedVideoFailure()
    {
        CompletePause();
        RewardVideoErrored?.Invoke(_requestID);
    }

    private void OnRewardedGame()
    {
        CompletePause();
        RewardVideoCompleted?.Invoke(_requestID);
        RewardVideoClosed?.Invoke(_requestID);
    }

    private void OnPauseGame()
    {
        BeginPause();
    }

    private void OnResumeGame()
    {
        CompletePause();
        if (_videoType == VideoType.Interstital)
            InterstitalVideoCompleted?.Invoke();
        Debug.Log("OnResumeGame()");
    }

#endif
    private enum VideoType
    {
        Not,
        Interstital,
        Rewarded
    }
}
