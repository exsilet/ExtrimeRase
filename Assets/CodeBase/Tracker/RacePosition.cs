using System.Collections.Generic;
using System.Linq;
using SO;
using TMPro;
using UI;
using UI.VictoryPanel;
using UnityEngine;

namespace Tracker
{
    public class RacePosition : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private int _totalLapsToFinish;
        [SerializeField] private List<Transform> _checkpoints;
        [SerializeField] private StartBattle _startBattle;
        [SerializeField] private PanelVictory _panelVictory;
        [SerializeField] private FinishLine _finishLine;

        private LapCounter _playerLapCounter;
        private int _maxOpponent;
        private HeroesData _heroes;
        private List<LapCounter> _opponentLapCounters = new List<LapCounter>();
        private List<RacerCheckpointData> _racerDataList = new List<RacerCheckpointData>();
        private int _nextCheckPoint;

        public int TotalLapFinish => _totalLapsToFinish;

        public List<RacerCheckpointData> RacerDataList => _racerDataList;

        public void InitializedEnemy(LapCounter enemy, HeroesData enemyData)
        {
            _opponentLapCounters.Add(enemy);
            _maxOpponent = _opponentLapCounters.Count - 1;

            if (_opponentLapCounters.Count >= _maxOpponent)
            {
                foreach (var opponent in _opponentLapCounters)
                {
                    if (!_racerDataList.Any(data => data.lapCounter == opponent))
                    {
                        _racerDataList.Add(new RacerCheckpointData { HeroesData = enemyData, lapCounter = opponent });
                    }
                }
            }
        }

        public void InitializedPlayer(LapCounter player, HeroesData playerData)
        {
            _playerLapCounter = player;
            _heroes = playerData;
            _racerDataList.Add(new RacerCheckpointData { HeroesData = playerData, lapCounter = _playerLapCounter });
            _panelVictory.FinishedLap(_totalLapsToFinish);
        }

        private void Start()
        {
            for (int i = 0; i < _checkpoints.Count; i++)
            {
                _checkpoints[i].GetComponent<Checkpoint>().Initialized(this, i);
            }

            _panelVictory.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_startBattle.CurrentStartBattle) return;

            UpdateRacerData();
            UpdateRacerPositions();
        }

        public Transform SwapPont(GameObject carLapCounter)
        {
            foreach (RacerCheckpointData racerData in _racerDataList)
            {
                if (racerData.lapCounter.name == carLapCounter.name)
                {
                    int numberPont = racerData.lastCheckpoint;
                    var spawnPosition = _checkpoints[numberPont];
                    return spawnPosition;
                }
            }

            return null;
        }

        public void RacerPassedCheckpoint(LapCounter racer, int checkpointIndex)
        {
            RacerCheckpointData racerData = _racerDataList.First(r => r.lapCounter == racer);

            if (checkpointIndex == (racerData.lastCheckpoint + 1) % _checkpoints.Count)
            {
                racerData.lastCheckpoint = checkpointIndex;
            }

            if (racerData.lastCheckpoint == _checkpoints.Count - 1)
            {
                racer.IncrementLap();
            }
        }

        private void UpdateRacerData()
        {
            foreach (var racerData in _racerDataList)
            {
                Transform nextCheckpoint = _checkpoints[(racerData.lastCheckpoint + 1) % _checkpoints.Count];
                racerData.distanceToNextCheckpoint =
                    Vector3.Distance(racerData.lapCounter.transform.position, nextCheckpoint.position);
            }
        }

        private void UpdateRacerPositions()
        {
            List<RacerCheckpointData> sortedRacers = _racerDataList.OrderByDescending(r => r.lapCounter.CurrentLap)
                .ThenByDescending(r => r.lastCheckpoint)
                .ThenBy(r => r.distanceToNextCheckpoint)
                .ToList();

            int playerPosition = sortedRacers.IndexOf(_racerDataList.First(r => r.lapCounter == _playerLapCounter)) + 1;
            _positionText.text = $"{playerPosition} / {_racerDataList.Count}";

            // if (_playerLapCounter.CurrentLap >= _totalLapsToFinish)
            // {
            //     _positionText.text = "Гонка завершена! Ваша позиция: " + playerPosition + " из " + _racerDataList.Count;
            //     _panelVictory.OpenPanel();
            // }
        }
    }
}