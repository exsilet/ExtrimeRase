﻿using System.Collections;
using ArcadeVP;
using DefaultNamespace;
using Hero;
using SaveData;
using SO;
using Tracker;
using UI;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private RacePosition _racePosition;
        [SerializeField] private StartBattle _startBattle;
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private HealthView _heroViewHealth;
        [SerializeField] private FollowTarget _followTarget;
        [SerializeField] private Minimap _minimapTarget;
        [SerializeField] private float _respawnDelay;
        [SerializeField] private AttackButton _attackButton;

        private HeroHealth _heroHealth;
        private IPersistentData _persistentPlayerData;
        private Player _hero;

        public void Initialized(IPersistentData persistentData, Player heroes)
        {
            _hero = heroes;
        }
        
        private void Start() => SpawnPlayer();
        
        private void OnEnable()
        {
            if (_heroHealth != null)
            {
                _heroHealth.Died += HeroHealthOnDied;
            }
        }
        
        private void OnDisable()
        {
            _heroHealth.Died -= HeroHealthOnDied;
        }

        private void SpawnPlayer()
        {
            GameObject car = Instantiate(_hero.gameObject, _spawnPosition);
            _heroHealth = car.GetComponent<HeroHealth>();
            _heroHealth.Died += HeroHealthOnDied;

            NewPlayerCar(car);
        }

        private void NewPlayerCar(GameObject car)
        {
            car.GetComponent<ArcadeVehicleController>().Initialized(_startBattle);
            car.GetComponent<Inventory>().Initialized(_weaponView);
            _attackButton.Initialized(car.GetComponent<Player>());
            
            _followTarget.Initialized(car.transform);
            _minimapTarget.Initialized(car.transform);
            _heroViewHealth.Initialized(_heroHealth);
            
            LapCounter carLapCounter = car.GetComponent<LapCounter>();
            _racePosition.InitializedPlayer(carLapCounter, _hero.Heroes);
        }

        private void HeroHealthOnDied(HeroHealth hero)
        {
            StartCoroutine(RespawnCarCoroutine(hero));
        }

        private IEnumerator RespawnCarCoroutine(HeroHealth hero)
        {
            yield return new WaitForSeconds(_respawnDelay);

            Debug.Log(" respawn ");
            
            Transform spawnPosition = _racePosition.SwapPont(hero.gameObject);
            
            Vector3 vector3 = hero.gameObject.transform.position;
            vector3 = spawnPosition.transform.position;
            hero.gameObject.transform.position = vector3;
            
            hero.ResetHealth();
            hero.gameObject.SetActive(true);
        }
    }
}