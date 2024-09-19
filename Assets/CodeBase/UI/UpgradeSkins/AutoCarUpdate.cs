using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hero;
using SaveData;
using UI.ShopSkins;
using UI.Visitor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UpgradeSkins
{
    [Serializable]
    public class CarCard
    {
        public List<GameObject> upgradeLevels;
    }

    public class AutoCarUpdate : MonoBehaviour
    {
        [SerializeField] private List<CarCard> _cars;
        [SerializeField] private Transform _carDisplayPoint;
        [SerializeField] private Transform _confirmationPanel;
        [SerializeField] private Button _nextCarButton;
        [SerializeField] private Button _previousCarButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private PurchasedCars _purchasedCars;
        [SerializeField] private UpgradePanel _upgradePanel;
        [SerializeField] private UpgradeContent _contentItems;

        private int _nextCar;
        private int _currentCarIndexShop = 0;
        private int _currentUpgradeLevel;
        private readonly int _maxLvlUpgrade = 5;

        private int[] _carUpgradeLevels;
        private GameObject _currentCarInstance;
        private ShopItem _currentShopItem;

        private IDataProvider _dataProvider;
        private PlayerMoney _playerMoney;
        private SkinUpdater _skinUpdater;
        private SkinSelector _skinSelector;

        private List<BuyCars> _buyCars = new();

        public void InitializeLoad(IDataProvider dataProvider, PlayerMoney playerMoney, SkinUpdater skinUpdater, SkinSelector skinSelector)
        {
            _dataProvider = dataProvider;
            _playerMoney = playerMoney;
            _skinUpdater = skinUpdater;
            _skinSelector = skinSelector;
        }

        public void Initialize(List<BuyCars> byuCars)
        {
            _buyCars = byuCars;

            DisplayCar();

            OnCharacterSkinsButtonClick();
        }

        private void Start()
        {
            _carUpgradeLevels = new int[_cars.Count];
            for (int i = 0; i < _cars.Count; i++)
            {
                _carUpgradeLevels[i] = 0;
            }
        }

        private void OnEnable()
        {
            _nextCarButton.onClick.AddListener(ShowNextCar);
            _previousCarButton.onClick.AddListener(ShowPreviousCar);
            _upgradeButton.onClick.AddListener(ShowConfirmationPanel);
        }

        private void OnDisable()
        {
            _nextCarButton.onClick.RemoveListener(ShowNextCar);
            _previousCarButton.onClick.RemoveListener(ShowPreviousCar);
            _upgradeButton.onClick.RemoveListener(ShowConfirmationPanel);
        }

        public void SetCurrentCar(int index, ShopItem shopItem)
        {
            _currentShopItem = shopItem;
            _currentCarIndexShop = index;
            DisplayCar();
        }

        public void OnYesButtonClicked()
        {
            var currentCar = GetBuyCar();
            ActiveButton(currentCar);

            if (_playerMoney.IsEnough(currentCar.ShopItem.Price))
            {
                _playerMoney.Spend(currentCar.ShopItem.Price);

                ApplyUpgrade();

                _confirmationPanel.gameObject.SetActive(false);
            }
        }

        private void DisplayCar()
        {
            if (_currentCarInstance != null)
            {
                Destroy(_currentCarInstance);
            }

            if (_buyCars.Count > 0)
            {
                BuyCars currentCar = GetBuyCar();
                int carLevel = GetCarLevel(currentCar);
                _currentCarInstance = Instantiate(_cars[_currentCarIndexShop].upgradeLevels[carLevel], _carDisplayPoint.position, _carDisplayPoint.rotation);
                _currentCarInstance.transform.SetParent(_carDisplayPoint);
            }
        }

        private BuyCars GetBuyCar()
        {
            // var listCars = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();
            //
            // foreach (ShopItem item in listCars)
            // {
            //     if (item == _currentShopItem)
            //     {
            //         
            //     }
            // }
            
            
            for (int i = 0; i < _buyCars.Count(); i++)
            {
                if (_buyCars[i].IndexModel == _currentCarIndexShop)
                {
                    return _buyCars[i];
                }
            }

            return null;
        }

        private int GetCarLevel(BuyCars buyCars)
        {
            return buyCars.LvlCar;
        }

        private void ApplyUpgrade()
        {
            var currentCar = GetBuyCar();
            int carLevel = GetCarLevel(currentCar);
            _currentUpgradeLevel = carLevel;

            if (_currentUpgradeLevel < _maxLvlUpgrade - 1)
            {
                _currentUpgradeLevel++;
                UpdateCarDisplay(_currentUpgradeLevel);

                StartCoroutine(BounceAnimation());
            }
        }

        private void UpdateCarDisplay(int lvlCar)
        {
            if (_currentCarInstance != null)
            {
                Destroy(_currentCarInstance);
            }

            if (_buyCars.Count > 0)
            {
                _currentCarInstance = Instantiate(_cars[_currentCarIndexShop].upgradeLevels[lvlCar], _carDisplayPoint.position, _carDisplayPoint.rotation);
                _currentCarInstance.transform.SetParent(_carDisplayPoint);

                ShopItem newLevelCar = _currentCarInstance.GetComponent<CharacterSkin>().ShopItems;
                
                _skinUpdater.Visit(newLevelCar, lvlCar);
                _skinSelector.Visit(newLevelCar);
                _dataProvider.Save();

                _purchasedCars.InitializeUpdateCarLvl(newLevelCar, _currentCarIndexShop);
            }
        }

        private void OnCharacterSkinsButtonClick()
        {
            BuyCars currentCar = GetBuyCar();

            _upgradePanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), currentCar.ShopItem);
        }

        private void ShowNextCar()
        {
            _nextCar++;
            if (_nextCar >= _buyCars.Count)
            {
                _nextCar = 0;
            }

            NextModelCar();

            BuyCars currentCar = GetBuyCar();
            _upgradePanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), currentCar.ShopItem);

            DisplayCar();
            ActiveButton(currentCar);
        }

        private void ShowPreviousCar()
        {
            _nextCar--;
            if (_nextCar < 0)
            {
                _nextCar = _buyCars.Count - 1;
            }

            NextModelCar();

            BuyCars currentCar = GetBuyCar();
            _upgradePanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), currentCar.ShopItem);

            DisplayCar();
            ActiveButton(currentCar);
        }

        private void NextModelCar()
        {
            for (int i = 0; i < _buyCars.Count; i++)
            {
                if (i == _nextCar)
                {
                    _currentCarIndexShop = _buyCars[i].IndexModel;
                }
            }
        }

        private void ShowConfirmationPanel()
        {
            _confirmationPanel.gameObject.SetActive(true);
        }

        private void ActiveButton(BuyCars cars)
        {
            _currentUpgradeLevel = cars.LvlCar;
            _upgradeButton.gameObject.SetActive(_currentUpgradeLevel < _maxLvlUpgrade - 1);
        }

        private IEnumerator BounceAnimation()
        {
            Vector3 originalPosition = _currentCarInstance.transform.localPosition;
            float bounceHeight = 2f;
            float bounceTime = 0.2f;

            // Подпрыгивание вверх
            float elapsedTime = 0;
            while (elapsedTime < bounceTime)
            {
                _currentCarInstance.transform.localPosition = Vector3.Lerp(originalPosition,
                    originalPosition + Vector3.forward * bounceHeight, (elapsedTime / bounceTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;
            while (elapsedTime < bounceTime)
            {
                _currentCarInstance.transform.localPosition = Vector3.Lerp(
                    originalPosition + Vector3.forward * bounceHeight, originalPosition, (elapsedTime / bounceTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _currentCarInstance.transform.localPosition = originalPosition;
        }
    }
}