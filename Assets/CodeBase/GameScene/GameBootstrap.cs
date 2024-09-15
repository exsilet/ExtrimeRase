using Hero;
using SaveData;
using Spawners;
using UI.MainMenu;
using UI.Visitor;
using UnityEngine;

namespace GameScene
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private CharacterFactory _characterFactory;
        [SerializeField] private ReturnToTheGarage _theGarage;
        
        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;
        private NextGameScene _nextGameScene;
        private PlayerScore _playerScore;

        private void Awake()
        {
            InitializeData();
            InitializeGameScene();
            InitializePlayer();
        }
        
        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void InitializeGameScene()
        {
            _nextGameScene = new NextGameScene(_persistentPlayerData);
            _playerScore = new PlayerScore(_persistentPlayerData);
            
            _theGarage.Initialize(_persistentPlayerData, _dataProvider, _nextGameScene, _playerScore);
        }

        private void InitializePlayer()
        {
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            Player hero = _characterFactory.Get(_persistentPlayerData.DataBase.SelectedCarSkin);
            
            _playerSpawner.Initialized(_characterFactory, _persistentPlayerData, hero);
        }
        
        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayerData.DataBase = new DataBase();
        }
    }
}