using Hero;
using SaveData;
using UI.MainMenu;
using UI.UpgradeSkins;
using UI.Visitor;
using UnityEngine;

namespace UI
{
    public class ShopBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private PurchasedCars _purchasedCars;
        [SerializeField] private AutoCarUpdate _autoCarUpdate;
        [SerializeField] private NextTrackGame _nextTrack;
        
        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;

        private PlayerMoney _wallet;
        private PlayerScore _playerScore;
        private NextGameScene _nextGameScene;

        public void Awake()
        {
            InitializeData();

            InitializeWallet();
            
            InitializeBuyCar();

            InitializeShop();
            
            InitializeUpdateCar();

            InitializeGameTrace();
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);

            LoadDataOrInit();
        }

        private void InitializeGameTrace()
        {
            _nextGameScene = new NextGameScene(_persistentPlayerData);
            _playerScore = new PlayerScore(_persistentPlayerData);
            
            _nextTrack.Initialize(_nextGameScene, _dataProvider, _playerScore);
        }

        private void InitializeWallet()
        {
            _wallet = new PlayerMoney(_persistentPlayerData);
        
            _walletView.Initialize(_wallet);
        }

        private void InitializeBuyCar()
        {
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
            OpenUpdateSkinsChecker openUpdateSkinsChecker = new OpenUpdateSkinsChecker(_persistentPlayerData);

            _purchasedCars.InitializeLoad(_dataProvider, openSkinsChecker, skinSelector, openUpdateSkinsChecker);
        }
        
        private void InitializeUpdateCar()
        {
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            SkinUpdater skinUpdater = new SkinUpdater(_persistentPlayerData);
            
            _autoCarUpdate.InitializeLoad(_dataProvider, _wallet, skinUpdater, skinSelector);
        }

        private void InitializeShop()
        {
            OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);
            SkinUpdater skinUpdater = new SkinUpdater(_persistentPlayerData);
            
            _shop.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker, skinUpdater);
        }

        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayerData.DataBase = new DataBase();
        }
    }
}