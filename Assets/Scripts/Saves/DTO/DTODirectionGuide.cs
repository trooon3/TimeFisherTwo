using System;

namespace Assets.Scripts.Saves.DTO
{
    [Serializable]
    public class DTODirectionGuide
    {
        private bool _isShowedWalkTutorial;
        private bool _isShowedCatchTutorial;
        private bool _isShowedGetFishTutorial;

        public bool IsShowedWalkTutorial => _isShowedWalkTutorial;
        public bool IsShowedCatchTutorial => _isShowedCatchTutorial;
        public bool IsShowedGetFishTutorial => _isShowedGetFishTutorial;

        public void Init(bool isShowedWalkTutorial, bool isShowedCatchTutorial, bool isShowedGetFishTutorial)
        {
            isShowedWalkTutorial = _isShowedWalkTutorial;
            isShowedCatchTutorial = _isShowedCatchTutorial;
            isShowedGetFishTutorial = _isShowedGetFishTutorial;
        }
    }
}

