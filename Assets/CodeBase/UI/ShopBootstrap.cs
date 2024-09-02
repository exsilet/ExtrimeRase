using Hero;
using SaveData;
using UI.ShopSkins;
using UI.Visitor;
using UnityEngine;

namespace UI
{
    public class ShopBootstrap : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private PurchasedCars _purchasedCars;
        
        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayerData;

        private PlayerMoney _wallet;

        public void Awake()
        {
            InitializeData();

            InitializeWallet();
            
            InitializeBuyCar();

            InitializeShop();
        }

        private void InitializeData()
        {
            _persistentPlayerData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayerData);

            LoadDataOrInit();
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
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            
            _purchasedCars.InitializeLoad(_persistentPlayerData, _dataProvider, openSkinsChecker, skinSelector, selectedSkinChecker);
        }

        private void InitializeShop()
        {
            OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
            SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentPlayerData);
            SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
            SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);
            
            _shop.Initialize(_persistentPlayerData, _dataProvider, _wallet, openSkinsChecker, selectedSkinChecker, skinSelector, skinUnlocker);
        }

        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayerData.DataBase = new DataBase();
        }
    }
}