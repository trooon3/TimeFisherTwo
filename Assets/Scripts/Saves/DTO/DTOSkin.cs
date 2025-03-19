using System;

namespace Assets.Scripts.Saves.DTO
{
    [Serializable]
    public class DTOSkin
    {
        private bool _isBuyed;
        private string _name;

        public bool IsBuyed => _isBuyed;
        public string Name => _name;

        public void Init(string name, bool isBuyed)
        {
            _name = name;
            _isBuyed = isBuyed;
        }
    }
}