using UnityEngine;

namespace Assets.Scripts.Saves
{
    public class SkinSaver : MonoBehaviour
    {
        public string CreateJson(SkinView skin)
        {
            string save = JsonUtility.ToJson(skin);
            return save;
        }
    }
}

