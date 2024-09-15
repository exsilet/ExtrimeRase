using System;
using SaveData;

namespace Hero
{
    public class PlayerScore
    {
        private IPersistentData _persistentData;

        public PlayerScore(IPersistentData persistentData)
            => _persistentData = persistentData;

        public void AddScore(int score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score));

            _persistentData.DataBase.ScoreGameTrack += score;
        }

        public int GetCurrentScore() => _persistentData.DataBase.ScoreGameTrack;

        public void Spend(int score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score));

            _persistentData.DataBase.ScoreGameTrack = score;
        }
    }
}