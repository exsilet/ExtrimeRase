using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using SaveData;
using UI.MainMenu;
using UI.ShopSkins;
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
        [SerializeField] private ShopContent _contentItems;
        [SerializeField] private SkinBuyPlacement _skinBuyPlacement;
        [SerializeField] private AutoCarUpdate _autoCarUpdate;
        [SerializeField] private Button _nextButtonCar;
        [SerializeField] private Button _backButtonCar;

        private List<BuyCars> _byuCars = new();
        private int _currentCarIndex = 0;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private OpenSkinsChecker _openSkinsChecker;
        private SkinSelector _skinSelector;
        private SelectedSkinChecker _selectedSkinChecker;
        private List<ShopItem> _shopItems;
        private ShopItem _shopItem;
        private ShopItemView _previewedItem;
        public List<BuyCars> ByuCarsList => _byuCars;

        public void InitializeLoad(IPersistentData persistentData, IDataProvider dataProvider,
            OpenSkinsChecker openSkinsChecker, SkinSelector skinsSelector, SelectedSkinChecker selectedSkinChecker)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            _openSkinsChecker = openSkinsChecker;
            _skinSelector = skinsSelector;
            _selectedSkinChecker = selectedSkinChecker;

            _shopItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();

            for (int i = 0; i < _shopItems.Count; i++)
            {
                _openSkinsChecker.Visit(_shopItems[i]);

                if (_openSkinsChecker.IsOpened)
                {
                    UpdateCarDisplay(_shopItems[i]);
                    Initialize(_shopItems[i], i);
                }
            }
        }

        public void Initialize(ShopItem shopItem, int index)
        {
            _shopItem = shopItem;
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

        private void OnSelectionButtonClick()
        {
            SelectSkin();

            _dataProvider.Save();
        }

        public void HideCars()
        {
            foreach (BuyCars car in _byuCars)
            {
                car.Model.SetActive(false);
            }
        }

        private void UpdateCarDisplay(ShopItem shopItem)
        {
            Clear();

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
                }
            }
        }

        private void SelectSkin()
        {
            //_skinSelector.Visit(_previewedItem.Item);
        }
    }
}