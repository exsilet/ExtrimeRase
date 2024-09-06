using SaveData;
using Spawners;
using UI.Visitor;
using UnityEngine;

namespace GameScene
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private CharacterFactory _characterFactory;
        
        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;

        private void Awake()
        {
            InitializeData();
            InitializePlayer();
        }
        
        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void InitializePlayer()
        {
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            var hero = _characterFactory.Get(_persistentPlayerData.DataBase.SelectedCarSkin);
            
            
            _playerSpawner.Initialized(_characterFactory, _persistentPlayerData, hero);
        }
        
        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayerData.DataBase = new DataBase();
        }
    }
}