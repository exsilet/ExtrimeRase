using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using UI.ShopSkins;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class AutoCarUpdate : MonoBehaviour
    {
        [Serializable]
        public class CarCard
        {
            public GameObject[] upgradeLevels; // Массив моделей машины для каждого уровня улучшений
        }

        [SerializeField] private CarCard[] _cars;
        [SerializeField] private Transform _carDisplayPoint;
        [SerializeField] private Button _nextCarButton;
        [SerializeField] private Button _previousCarButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _confirmationPanel;
        [SerializeField] private PurchasedCars _purchasedCars;

        private int _currentCarIndex = 0;
        private int[] _carUpgradeLevels;
        private GameObject _currentCarInstance;
        private SlotCarData _currentCarData;
        private ShopItem _currentShopItem;
        private List<ShopItem> _shopItems = new();

        public void Initialize(ShopItem shopItem)
        {
            _shopItems.Add(shopItem);
        }

        private void Start()
        {
            _carUpgradeLevels = new int[_cars.Length];
            for (int i = 0; i < _cars.Length; i++)
            {
                _carUpgradeLevels[i] = 0;
            }

            UpdateCarDisplay();

            _confirmationPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _nextCarButton.onClick.AddListener(ShowNextCar);
            _previousCarButton.onClick.AddListener(ShowPreviousCar);
            _upgradeButton.onClick.AddListener(ShowConfirmationPanel);
            _backButton.onClick.AddListener(ReturnToGarage);
        }

        private void OnDisable()
        {
            _nextCarButton.onClick.RemoveListener(ShowNextCar);
            _previousCarButton.onClick.RemoveListener(ShowPreviousCar);
            _upgradeButton.onClick.RemoveListener(ShowConfirmationPanel);
            _backButton.onClick.RemoveListener(ReturnToGarage);
        }

        private void UpdateCarDisplay()
        {
            if (_currentCarInstance != null)
            {
                Destroy(_currentCarInstance);
            }

            int upgradeLevel = _carUpgradeLevels[_currentCarIndex];
            _currentCarInstance = Instantiate(_cars[_currentCarIndex].upgradeLevels[upgradeLevel], _carDisplayPoint.position, _carDisplayPoint.rotation);
            _currentCarInstance.transform.SetParent(_carDisplayPoint);

            CharacterSkinItem carLevel = _cars[_currentCarIndex].upgradeLevels[_currentCarIndex].GetComponent<UpgradeCar>().CarSkinItem;

            Debug.Log("  current car " + carLevel);
            Debug.Log(" shop car " + _shopItems[_currentCarIndex]);

            if (carLevel == _shopItems[_currentCarIndex])
            {
                CharacterSkinItem newLevelCar = _currentCarInstance.GetComponent<UpgradeCar>().CarSkinItem;
                Debug.Log(" new level car " + newLevelCar);
                _purchasedCars.InitializeUpdateCarLvl(_shopItems[_currentCarIndex], newLevelCar, _currentCarIndex);
            }
        }

        private void ShowNextCar()
        {
            _currentCarIndex = (_currentCarIndex + 1) % _cars.Length;
            UpdateCarDisplay();
            ActiveButton();
        }

        private void ShowPreviousCar()
        {
            _currentCarIndex = (_currentCarIndex - 1 + _cars.Length) % _cars.Length;
            UpdateCarDisplay();
            ActiveButton();
        }

        private void ShowConfirmationPanel()
        {
            _confirmationPanel.SetActive(true);
        }

        public void OnYesButtonClicked()
        {
            ApplyUpgrade();
            _confirmationPanel.SetActive(false);

            ActiveButton();
        }

        public void OnNoButtonClicked()
        {
            _confirmationPanel.SetActive(false);
        }

        private void ActiveButton()
        {
            int currentUpgradeLevel = _carUpgradeLevels[_currentCarIndex];

            _upgradeButton.gameObject.SetActive(currentUpgradeLevel < _cars[_currentCarIndex].upgradeLevels.Length - 1);
        }

        private void ApplyUpgrade()
        {
            int currentUpgradeLevel = _carUpgradeLevels[_currentCarIndex];
            if (currentUpgradeLevel < _cars[_currentCarIndex].upgradeLevels.Length - 1)
            {
                _carUpgradeLevels[_currentCarIndex]++;
                UpdateCarDisplay();

                StartCoroutine(BounceAnimation());
            }
        }

        private void ReturnToGarage()
        {
            Debug.Log("Возврат на панель гаража");
        }

        private IEnumerator BounceAnimation()
        {
            Vector3 originalPosition = _currentCarInstance.transform.localPosition;
            float bounceHeight = 2f; // высота подпрыгивания
            float bounceTime = 0.2f; // время на подпрыгивание

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