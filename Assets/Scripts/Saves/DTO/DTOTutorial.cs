using System;

namespace Assets.Scripts.Saves.DTO
{
    [Serializable]
    public class DTOTutorial
    {
        private bool _isShowed;
        public bool IsShowed => _isShowed;

        public void Init(bool isShowed)
        {
            _isShowed = isShowed;
        }
    }
}

