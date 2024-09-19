using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Hero;
using SaveData;
using UI.ShopSkins;
using UI.UpgradeSkins;
using UI.Visitor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopContent _contentItems;

        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Button _nextButtonCar;
        [SerializeField] private Button _backButtonCar;

        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private AutoCarUpdate _autoCarUpdate;

        [SerializeField] private SkinPlacement _skinPlacement;
        [SerializeField] private PurchasedCars _purchasedCars;

        private IDataProvider _dataProvider;
        
        private List<ShopItem> _shopItems;
        private ShopItemView _previewedItem;
        private PlayerMoney _wallet;
        private int _currentCarIndex = 0;

        private SkinSelector _skinSelector;
        private SkinUnlocker _skinUnlocker;
        private OpenSkinsChecker _openSkinsChecker;
        private SkinUpdater _skinUpdater;

        private void OnEnable()
        {
            _buyButton.Click += OnBuyButtonClick;
            _nextButtonCar.onClick.AddListener(ShowNextCar);
            _backButtonCar.onClick.AddListener(ShowPreviousCar);
            _shopPanel.ItemView += OnItemView;
        }

        private void OnDisable()
        {
            _shopPanel.ItemView -= OnItemView;
            _buyButton.Click -= OnBuyButtonClick;
            _nextButtonCar.onClick.RemoveListener(ShowNextCar);
            _backButtonCar.onClick.RemoveListener(ShowPreviousCar);
        }

        public void Initialize(IDataProvider dataProvider, PlayerMoney wallet, OpenSkinsChecker openSkinsChecker,
            SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker, SkinUpdater skinUpdater)
        {
            _wallet = wallet;
            _openSkinsChecker = openSkinsChecker;
            _skinSelector = skinSelector;
            _skinUnlocker = skinUnlocker;
            _skinUpdater = skinUpdater;

            _dataProvider = dataProvider;

            _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);

            _shopPanel.ItemView += OnItemView;

            OnCharacterSkinsButtonClick();
        }

        private void OnItemView(ShopItemView item)
        {
            _previewedItem = item;
            _skinPlacement.InstantiateModel(_previewedItem.Model, _currentCarIndex);

            _openSkinsChecker.Visit(_previewedItem.Item);

            if (_openSkinsChecker.IsOpened)
            {
                HideBuyButton();
            }
            else
            {
                ShowBuyButton(_previewedItem.Price);
            }
        }

        private void OnBuyButtonClick()
        {
            if (_wallet.IsEnough(_previewedItem.Price))
            {
                _wallet.Spend(_previewedItem.Price);
                _skinUnlocker.Visit(_previewedItem.Item);

                SelectSkin();

                _previewedItem.Unlock();
                
                _purchasedCars.Initialize(_previewedItem.Item, _currentCarIndex);
                _dataProvider.Save();
            }
        }

        private void OnCharacterSkinsButtonClick()
        {
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void ShowNextCar()
        {
            _shopItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();

            _currentCarIndex++;
            
            if (_currentCarIndex >= _shopItems.Count)
            {
                _currentCarIndex = 0;
            }
            
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void ShowPreviousCar()
        {
            _shopItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();
            
            _currentCarIndex--;
            if (_currentCarIndex < 0)
            {
                _currentCarIndex = _shopItems.Count - 1;
            }
            
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void SelectSkin()
        {
            _skinSelector.Visit(_previewedItem.Item);
            _skinUpdater.Visit(_previewedItem.Item, _previewedItem.LvlCar);
            HideBuyButton();
        }

        private void ShowBuyButton(int price)
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.UpdateText(price);

            if (_wallet.IsEnough(price))
                _buyButton.Unlock();
            else
                _buyButton.Lock();
        }

        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    }
}