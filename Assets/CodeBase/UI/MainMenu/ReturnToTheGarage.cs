using Hero;
using SaveData;
using UI.VictoryPanel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ReturnToTheGarage : MonoBehaviour
    {
        private const string Garage = "Гараж(меню)";
        
        [SerializeField] private Button _returnSceneButton;
        [SerializeField] private PanelVictory _panelVictory;

        private int _currentTrackIndex;
        private int _score;
        private NextGameScene _nextGame;
        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private PlayerScore _playerScore;
        private int _moneyRacePlayer;
        private PlayerMoney _playerMoney;

        public void Initialize(IPersistentData persistentData, IDataProvider dataProvider, NextGameScene nextGameScene, PlayerScore playerScore, PlayerMoney playerMoney)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            _nextGame = nextGameScene;
            _playerScore = playerScore;
            _playerMoney = playerMoney;
            
            _currentTrackIndex = _nextGame.GetCurrentSceneGame();
        }

        private void Start()
        {
            _returnSceneButton.onClick.AddListener(Return);
        }

        private void OnDestroy()
        {
            _returnSceneButton.onClick.RemoveListener(Return);
        }
        
        private void Return()
        {
            _currentTrackIndex++;
            _score = _panelVictory.PlayerScoreRace;
            _moneyRacePlayer = _panelVictory.PlayerScoreRace;
            _nextGame.NextTrace(_currentTrackIndex);
            _playerScore.AddScore(_score);
            _playerMoney.AddCoins(_moneyRacePlayer);
            _dataProvider.Save();
            SceneManager.LoadScene(Garage);
        }
    }
}