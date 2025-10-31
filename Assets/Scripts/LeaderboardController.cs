using UnityEngine;
using YG;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard;
    private int _countAllCatchedFishes;

    public void AddScore()
    {
        _countAllCatchedFishes++;
        if (YandexGame.auth)
        {
            _leaderboard.NewScore(_countAllCatchedFishes);
            YandexGame.NewLeaderboardScores(_leaderboard.nameLB, _countAllCatchedFishes);
        }
    }
}
