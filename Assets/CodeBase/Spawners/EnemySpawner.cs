using System.Collections;
using System.Collections.Generic;
using ArcadeVP;
using Enemy;
using SO;
using Tracker;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<CarSpawner> _spawnPoints;
        [SerializeField] private List<HeroesData> _enemies;
        [SerializeField] private RacePosition _racePosition;
        [SerializeField] private StartBattle _startBattle;
        [SerializeField] private List<WaypointCircuit> _waypointCircuit;
        [SerializeField] private float _respawnDelay;
        [SerializeField] private float _timerStartRespawn;

        private List<EnemyHealth> _enemyHealth = new();
        private int _currentSpawnPointIndex = 0;
        private float _timer;
        private float _speedThreshold = 0.5f;
        private float _speedRespawn = 3f;

        private void Start()
        {
            _timer = _timerStartRespawn;
            SpawnEnemies();
        }

        private void Update()
        {
            RespawnStuck();
        }

        private void OnEnable()
        {
            foreach (EnemyHealth enemyHealth in _enemyHealth)
                enemyHealth.DiedEnemy += CarHealthOnDiedEnemy;
        }

        private void OnDisable()
        {
            foreach (EnemyHealth enemyHealth in _enemyHealth)
                enemyHealth.DiedEnemy -= CarHealthOnDiedEnemy;
        }

        private void RespawnStuck()
        {
            _timer -= Time.deltaTime;
            
            if (_startBattle.CurrentStartBattle)
            {
                if (_timer <= 0)
                {
                    foreach (EnemyHealth enemy in _enemyHealth)
                    {
                        if (enemy.GetComponent<WaypointProgressTracker>().Speed <= _speedThreshold)
                        {
                            enemy.gameObject.SetActive(false);
                            CarHealthOnDiedEnemy(enemy);
                            _timer = _speedRespawn;
                        }
                    }
                }
            }
        }

        private void SpawnEnemies()
        {
            foreach (var enemyData in _enemies)
            {
                CarSpawner spawnPoint = _spawnPoints[_currentSpawnPointIndex];
                EnemyHealth enemy = Instantiate(enemyData.CarPrefab, spawnPoint.transform).GetComponent<EnemyHealth>();
                enemy.DiedEnemy += CarHealthOnDiedEnemy;
                _enemyHealth.Add(enemy);

                enemy.GetComponent<ArcadeAiVehicleController>().Initialized(_startBattle);

                var waypoint = enemy.GetComponent<WaypointProgressTracker>();
                enemy.GetComponent<SwapPosition>().Initialized(waypoint, _waypointCircuit, _currentSpawnPointIndex);

                LapCounter carLapCounter = enemy.GetComponent<LapCounter>();
                _racePosition.InitializedEnemy(carLapCounter);

                _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;
            }
        }

        private void CarHealthOnDiedEnemy(EnemyHealth enemy) =>
            StartCoroutine(RespawnCarCoroutine(enemy));

        private IEnumerator RespawnCarCoroutine(EnemyHealth enemy)
        {
            yield return new WaitForSeconds(_respawnDelay);

            enemy.ResetHealth();
            Transform spawnPosition = _racePosition.SwapPont(enemy.gameObject);
            var vectorRotation = spawnPosition.rotation;
            Vector3 vector3 = enemy.gameObject.transform.position;
            vector3 = spawnPosition.transform.position;
            enemy.gameObject.transform.position = vector3;

            enemy.gameObject.SetActive(true);
        }
    }
}