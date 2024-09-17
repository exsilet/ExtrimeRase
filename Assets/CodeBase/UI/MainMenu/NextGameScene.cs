using System;
using SaveData;

namespace UI.MainMenu
{
    public class NextGameScene
    {
        private IPersistentData _persistentData;
        
        public NextGameScene(IPersistentData persistentData)
            => _persistentData = persistentData;
        
        public int GetCurrentSceneGame() => _persistentData.DataBase.IndexScene;

        public int GetCurrentLevel() => _persistentData.DataBase.LevelGame;

        public void NextTrace(int trace)
        {
            if (trace < 0)
                throw new ArgumentOutOfRangeException(nameof(trace));

            _persistentData.DataBase.IndexScene = trace;
        }
        
        public void NextLevel(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level));

            _persistentData.DataBase.ScoreGameTrack += level;
        }
    }
}