using UnityEngine;

namespace Assets.Scripts.Saves
{
    public class LevelSaver : MonoBehaviour
    {
        public string CreateJson(int level)
        {
            string save = JsonUtility.ToJson(level);
            return save;
        }
    }
}

