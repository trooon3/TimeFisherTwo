using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class AutorizedCheker : MonoBehaviour
    {
        [SerializeField] private GameObject _autorizedOfferWindow;
        [SerializeField] private GameObject _leaderboardWindow;

        public void ShowOfferAutorized()
        {
            if (!YandexGame.auth)
            {
                _autorizedOfferWindow.SetActive(true);
            }
            else
            {
                _leaderboardWindow.SetActive(true);
            }
        }
    }
}

