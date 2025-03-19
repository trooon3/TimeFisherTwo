using System;

namespace Assets.Scripts.Saves.DTO
{
    [Serializable]

    public class DTOLevel
    {
        private int _level;
        private int _count;
        private int _score;

        public int Level => _level;
        public int Count => _count;
        public int Score => _score;

        public void Init(int level, int count, int score)
        {
            _count = count;
            _level = level;
            _score = score;
        }

        public void Init(int level, int count)
        {
            _count = count;
            _level = level;
        }
    }
}