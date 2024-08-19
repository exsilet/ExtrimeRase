using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI;
using UnityEngine;

namespace Tracker
{
    public class RacePosition : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private int _totalLapsToFinish;
        [SerializeField] private List<Transform> _checkpoints;
        [SerializeField] private StartBattle _startBattle;

        private LapCounter _playerLapCounter;
        private readonly int _maxOpponent = 6;
        private List<LapCounter> _opponentLapCounters = new List<LapCounter>();
        private List<RacerCheckpointData> _racerDataList = new List<RacerCheckpointData>();

        public List<RacerCheckpointData> RacerDataList => _racerDataList;
        
        public void InitializedEnemy(LapCounter enemy)
        {
            _opponentLapCounters.Add(enemy);

            if (_opponentLapCounters.Count >= _maxOpponent)
            {
                foreach (var opponent in _opponentLapCounters)
                {
                    _racerDataList.Add(new RacerCheckpointData { racer = opponent });
                }
            }
        }
        
        public void InitializedPlayer(LapCounter player)
        {
            _playerLapCounter = player;
            _racerDataList.Add(new RacerCheckpointData { racer = _playerLapCounter });
        }
        
        private void Start()
        {
            for (int i = 0; i < _checkpoints.Count; i++)
            {
                _checkpoints[i].GetComponent<Checkpoint>().Initialized(this, i);
            }
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
                if (racerData.racer.name == carLapCounter.name)
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
            RacerCheckpointData racerData = _racerDataList.First(r => r.racer == racer);
            if (checkpointIndex == (racerData.lastCheckpoint + 1) % _checkpoints.Count)
            {
                racerData.lastCheckpoint = checkpointIndex;
            }
        }

        private void UpdateRacerData()
        {
            foreach (var racerData in _racerDataList)
            {
                Transform nextCheckpoint = _checkpoints[(racerData.lastCheckpoint + 1) % _checkpoints.Count];
                racerData.distanceToNextCheckpoint =
                    Vector3.Distance(racerData.racer.transform.position, nextCheckpoint.position);
            }
        }

        private void UpdateRacerPositions()
        {
            var sortedRacers = _racerDataList.OrderByDescending(r => r.racer.CurrentLap)
                .ThenByDescending(r => r.lastCheckpoint)
                .ThenBy(r => r.distanceToNextCheckpoint)
                .ToList();

            int playerPosition = sortedRacers.IndexOf(_racerDataList.First(r => r.racer == _playerLapCounter)) + 1;
            _positionText.text = $"{playerPosition} / {_racerDataList.Count}";

            if (_playerLapCounter.CurrentLap >= _totalLapsToFinish)
            {
                _positionText.text = "Гонка завершена! Ваша позиция: " + playerPosition + " из " + _racerDataList.Count;
                //добавить открытие панели выйгрыша, и пауза игры.
            }
        }
    }
}