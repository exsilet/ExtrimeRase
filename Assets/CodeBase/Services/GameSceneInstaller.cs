using AssetManagement;
using Hero;
using SO;
using UnityEngine;
using Zenject;

namespace Services
{
    public sealed class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private HeroesData _playerConfig;
        [SerializeField] private HeroesData _enemy1Config;
        [SerializeField] private HeroesData _enemy2Config;
        [SerializeField] private HeroesData _enemy3Config;
        [SerializeField] private Transform _spawnPlayer;
        
        public override void InstallBindings()
        {
            Container.Bind<HeroesData>().AsSingle();
            Container.Bind<AssetProvider>().AsSingle();
        }

        public override void Start()
        {
            // Container.Bind<HeroesData>().FromInstance(_playerConfig);
            // var player = Container.InstantiatePrefabForComponent<Player>(_playerConfig.CarPrefab, _spawnPlayer.position,
            //     Quaternion.identity, null);
        }
    }
}