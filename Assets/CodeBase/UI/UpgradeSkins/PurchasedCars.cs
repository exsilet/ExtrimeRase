using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using SaveData;
using UI.ShopSkins;
using UI.StartGameUI;
using UI.Visitor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UpgradeSkins
{
    [Serializable]
    public class BuyCars
    {
        public ShopItem ShopItem;
        public GameObject Model;
        public int LvlCar;
        public int IndexModel;

        public BuyCars(ShopItem shopItem, GameObject model, int lvlCar, int indexModel)
        {
            ShopItem = shopItem;
            Model = model;
            LvlCar = lvlCar;
            IndexModel = indexModel;
        }
    }

    public class PurchasedCars : MonoBehaviour
    {
        [SerializeField] private Transform _carDisplayPoint;

        //[SerializeField] private ShopContent _contentItems;
        [SerializeField] private UpgradeContent _contentItems;
        [SerializeField] private SkinBuyPlacement _skinBuyPlacement;
        [SerializeField] private AutoCarUpdate _autoCarUpdate;
        [SerializeField] private StartGamePlay _startGame;
        [SerializeField] private Button _nextButtonCar;
        [SerializeField] private Button _backButtonCar;

        private List<BuyCars> _byuCars = new();
        private int _currentCarIndex = 0;

        private List<ShopItem> _shopItems;
        private ShopItemView _previewedItem;
        private IDataProvider _dataProvider;
        private OpenSkinsChecker _openSkinsChecker;
        private SkinSelector _skinSelector;
        private OpenUpdateSkinsChecker _openUpdateSkins;

        public void InitializeLoad(IDataProvider dataProvider,
            OpenSkinsChecker openSkinsChecker, SkinSelector skinsSelector, OpenUpdateSkinsChecker openUpdateSkinsChecker)
        {
            _dataProvider = dataProvider;
            _openSkinsChecker = openSkinsChecker;
            _skinSelector = skinsSelector;
            _openUpdateSkins = openUpdateSkinsChecker;

            _shopItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();

            foreach (var item in _shopItems)
            {
                _openUpdateSkins.Visit(item, item.LvlCar);

                if (_openUpdateSkins.IsOpened)
                {
                    UpdateCarDisplay(item);
                    Initialize(item, _openUpdateSkins.IndexModel);
                }
            }

            _startGame.Initialize(_skinSelector, _dataProvider);
        }

        public void Initialize(ShopItem shopItem, int index)
        {
            _byuCars.Add(new BuyCars(shopItem, shopItem.Model, shopItem.LvlCar, index));

            _autoCarUpdate.SetCurrentCar(index);
            _autoCarUpdate.Initialize(_byuCars);

            UpdateCarDisplay(shopItem);
        }

        public void InitializeUpdateCarLvl(ShopItem newLevelCar, int indexCurrentCar)
        {
            _byuCars.RemoveAll(x => x.IndexModel == indexCurrentCar);

            _byuCars.Add(new BuyCars(newLevelCar, newLevelCar.Model, newLevelCar.LvlCar, indexCurrentCar));

            _autoCarUpdate.Initialize(_byuCars);
            UpdateCarDisplay(newLevelCar);
        }

        private void OnEnable()
        {
            _nextButtonCar.onClick.AddListener(ShowNextCar);
            _backButtonCar.onClick.AddListener(ShowPreviousCar);
        }

        private void OnDisable()
        {
            _nextButtonCar.onClick.RemoveListener(ShowNextCar);
            _backButtonCar.onClick.RemoveListener(ShowPreviousCar);
        }

        private void UpdateCarDisplay(ShopItem shopItem)
        {
            Clear();

            _startGame.CurrentCar(shopItem);
            _skinBuyPlacement.InstantiateModel(shopItem.Model);
        }

        private void Clear()
        {
            foreach (Transform child in _carDisplayPoint)
            {
                Destroy(child.gameObject);
            }
        }

        private void ShowNextCar()
        {
            _currentCarIndex++;

            if (_currentCarIndex >= _byuCars.Count)
            {
                _currentCarIndex = 0;
            }

            NextCarModel();
        }

        private void ShowPreviousCar()
        {
            _currentCarIndex--;
            if (_currentCarIndex < 0)
            {
                _currentCarIndex = _byuCars.Count - 1;
            }

            NextCarModel();
        }

        private void NextCarModel()
        {
            for (int i = 0; i < _byuCars.Count; i++)
            {
                if (i == _currentCarIndex)
                {
                    _skinBuyPlacement.InstantiateModel(_byuCars[i].Model);
                    _autoCarUpdate.SetCurrentCar(_byuCars[i].IndexModel);
                    _startGame.CurrentCar(_byuCars[i].ShopItem);
                }
            }
        }
    }
}