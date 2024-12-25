using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts.ScripsForWeb.Leaderboard
{
    public class YandexLeaderboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private GameObject _offerLogInPanel;

        private void OfferLogIn()
        {
            _offerLogInPanel.SetActive(true);
        }

        public void OpenLeaderBoard()
        {
            if (!YandexGame.auth)
            {
                _leaderboardView.gameObject.SetActive(false);
                OfferLogIn();
            }
        }

        public void Authorize()
        {
            YandexGame.AuthDialog();
        }

        public void SetPlayerScore(int score)
        {
            if (!YandexGame.auth)
            {
                return;
            }

            YandexGame.NewLeaderboardScores(LeaderboardName, score);
        }

        public void Fill()
        {
            if (!YandexGame.auth)
            {
                return;
            }

            _leaderboardPlayers.Clear();

            LeaderboardYG leaderboard = new LeaderboardYG();
            YandexGame.GetLeaderboard(LeaderboardName, 20, 3, 5, "hhh");
            leaderboard.UpdateLB();
        }
    }
}

