using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Assets.Scripts.ScripsForWeb
{
    public class YGInitAwater : MonoBehaviour
    {
        void Update()
        {
            if(YandexGame.SDKEnabled == true)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}