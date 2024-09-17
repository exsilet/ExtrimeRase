using System.Collections.Generic;
using Hero;
using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class NextTrackGame : MonoBehaviour
    {
        [SerializeField] private Button _nextGameSceneButton;
        [SerializeField] private List<int> _oneCompanyScene;
        [SerializeField] private List<int> _secondCompanyScene;
        [SerializeField] private List<int> _thirdCompanyScene;
        [SerializeField] private List<int> _fourthCompanyScene;
        [SerializeField] private List<int> _fifthCompanyScene;
        [SerializeField] private Transform _panelNewLevel;

        private string _nameSceneGame;
        private NextGameScene _nextGameScene;
        private IDataProvider _dataProvider;

        private int _currentTrackIndex = 1, _level = 1, _currentLevel;
        private int _currentScore;
        private int _finishedIndexScene = 8;

        public void Initialize(NextGameScene nextGame, IDataProvider dataProvider, PlayerScore playerScore)
        {
            _nextGameScene = nextGame;
            _dataProvider = dataProvider;
            _currentScore = playerScore.GetCurrentScore();

            _currentTrackIndex = nextGame.GetCurrentSceneGame();
            _level = nextGame.GetCurrentLevel();
            
            NextSceneGame(_level, _currentTrackIndex);
            FinishedLevel(playerScore);
        }

        private void Start()
        {
            _nextGameSceneButton.onClick.AddListener(GameScene);
        }

        private void OnDestroy()
        {
            _nextGameSceneButton.onClick.RemoveListener(GameScene);
        }

        private void NextSceneGame(int level, int indexScene)
        {
            switch (level)
            {
                case 1:
                    CurrentNextScene(indexScene, _oneCompanyScene);
                    break;
                case 2:
                    CurrentNextScene(indexScene, _secondCompanyScene);
                    break;
                case 3:
                    CurrentNextScene(indexScene, _thirdCompanyScene);
                    break;
                case 4:
                    CurrentNextScene(indexScene, _fourthCompanyScene);
                    break;
                case 5:
                    CurrentNextScene(indexScene, _fifthCompanyScene);
                    break;
            }
        }

        private void CurrentNextScene(int indexScene, List<int> companyScene)
        {
            for (int i = 0; i < companyScene.Count; i++)
            {
                if (i == indexScene - 1)
                {
                    _currentTrackIndex = companyScene[i];
                    Debug.Log(" next scene " + _currentTrackIndex);
                    break;
                }
            }
        }

        private void FinishedLevel(PlayerScore playerScore)
        {
            if (_currentTrackIndex > _finishedIndexScene)
            {
                _currentTrackIndex = 1;
                playerScore.Spend(0);
                _currentLevel += _level;
                _nextGameScene.NextLevel(_currentLevel);
                _nextGameScene.NextTrace(_currentTrackIndex);
                _panelNewLevel.gameObject.SetActive(true);
            }
        }

        private void GameScene()
        {
            _dataProvider.Save();
            _nameSceneGame = "Level" + _level + "_" + _currentTrackIndex;
            SceneManager.LoadScene(_nameSceneGame);
        }
    }
}