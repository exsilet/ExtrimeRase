using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using SaveData;
using UI.Visitor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class PurchasedCars : MonoBehaviour
    {
        [Serializable]
        public class ByuCars
        {
            public ShopItem ShopItem;
            public GameObject Model;

            public ByuCars(ShopItem shopItem, GameObject model)
            {
                ShopItem = shopItem;
                Model = model;
            }
        }

        [SerializeField] private Transform _carDisplayPoint;
        [SerializeField] private ShopContent _contentItems;
        [SerializeField] private SkinBuyPlacement _skinBuyPlacement;
        [SerializeField] private Button _nextButtonCar;
        [SerializeField] private Button _backButtonCar;

        private List<ByuCars> _byuCars = new();
        private int _currentCarIndex = 0;
        private int[] _carUpgradeLevels;

        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private OpenSkinsChecker _openSkinsChecker;
        private SkinSelector _skinSelector;
        private SelectedSkinChecker _selectedSkinChecker;
        private List<ShopItem> _shopItems;
        private ShopItem _shopItem;
        private ShopItemView _previewedItem;

        public void InitializeLoad(IPersistentData persistentData, IDataProvider dataProvider,
            OpenSkinsChecker openSkinsChecker, SkinSelector skinsSelector, SelectedSkinChecker selectedSkinChecker)
        {
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            _openSkinsChecker = openSkinsChecker;
            _skinSelector = skinsSelector;
            _selectedSkinChecker = selectedSkinChecker;

            _shopItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();

            foreach (ShopItem item in _shopItems)
            {
                _openSkinsChecker.Visit(item);

                if (_openSkinsChecker.IsOpened)
                {
                    UpdateCarDisplay(item);
                    Initialize(item);
                }
            }
        }

        public void Initialize(ShopItem shopItem)
        {
            _shopItem = shopItem;
            _byuCars.Add(new ByuCars(shopItem, shopItem.Model));

            UpdateCarDisplay(shopItem);
        }

        public void InitializeUpdateCarLvl(ShopItem shopItem, ShopItem newLevelCar, int index)
        {
            if (_byuCars[index].ShopItem == shopItem)
            {
                Debug.Log(" delete car ");
                _byuCars.Remove(_byuCars[index]);
                _byuCars.Add(new ByuCars(newLevelCar, newLevelCar.Model));
                UpdateCarDisplay(newLevelCar);
            }
            else
            {
                _byuCars.Remove(_byuCars[index]);
                _byuCars.Add(new ByuCars(newLevelCar, newLevelCar.Model));
                UpdateCarDisplay(newLevelCar);
            }
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
            foreach (ByuCars car in _byuCars)
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
                }
            }
        }

        private void SelectSkin()
        {
            //_skinSelector.Visit(_previewedItem.Item);
        }
    }
}