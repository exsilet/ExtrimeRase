using System;
using System.Collections.Generic;
using SO;
using UI.MainMenu;
using UnityEngine;

namespace UI.VictoryPanel
{
    public class PanelVictory : MonoBehaviour
    {
        [SerializeField] private Transform _panelExpectations;
        [SerializeField] private Transform _textExpectations;
        [SerializeField] private Transform _panelVictory;
        [SerializeField] private List<TopPlayerGame> _playerGames;
        [SerializeField] private ScoreViewGame _scoreView;
        [SerializeField] private float _waitForLeaders = 5f;

        private int _topThreePlayers = 2;
        private int _money;
        private int _finishingLap;
        private int _playerScoreRace;

        private List<HeroesData> _heroes = new();

        public int FinishingLap => _finishingLap;
        public int PlayerScoreRace => _playerScoreRace;

        public void Initialized(HeroesData heroes) =>
            _heroes.Add(heroes);

        public void FinishedLap(int finishingLap) =>
            _finishingLap = finishingLap;

        private void Start()
        {
            _textExpectations.gameObject.SetActive(true);
            _scoreView.Initialize();
            _panelVictory.gameObject.SetActive(false);
            Invoke("ShowWinnersAfterDelay", _waitForLeaders);
        }

        public void OpenPanel()
        {
            _panelExpectations.gameObject.SetActive(true);
        }

        private void ShowWinnersAfterDelay()
        {
            ShowWinners();
            CurrentLocation();
        }

        private void ShowWinners()
        {
            _panelVictory.gameObject.SetActive(true);
            _textExpectations.gameObject.SetActive(false);

            for (int i = 0; i < Math.Min(_heroes.Count, _topThreePlayers + 1); i++)
            {
                if (i <= _topThreePlayers)
                {
                    _playerGames[i].Initialized(_heroes[i].IconPlayer);
                }
            }

            CancelInvoke();
        }

        private void CurrentLocation()
        {
            for (int i = 0; i < Math.Min(_heroes.Count, _topThreePlayers + 1); i++)
            {
                if (_heroes[i].HeroesTypeID == HeroesTypeID.CurrentPlayer)
                {
                    _playerScoreRace = _scoreView.GetScorePlace(i + 1);
                }
            }
        }
    }
}