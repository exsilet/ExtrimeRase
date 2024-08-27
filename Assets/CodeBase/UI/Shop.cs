using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Hero;
using SaveData;
using UI.ShopSkins;
using UI.Visitor;
using UnityEngine;
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

        [SerializeField] private SkinPlacement _skinPlacement;

        private IDataProvider _dataProvider;

        private ShopItemView _previewedItem;

        private PlayerMoney _wallet;

        private SkinSelector _skinSelector;
        private SkinUnlocker _skinUnlocker;
        private OpenSkinsChecker _openSkinsChecker;
        private SelectedSkinChecker _selectedSkinChecker;
        private int _currentCarIndex = 0;

        private List<ShopItem> contentItems;

        private void OnEnable()
        {
            _buyButton.Click += OnBuyButtonClick;
            _nextButtonCar.onClick.AddListener(ShowNextCar);
            _backButtonCar.onClick.AddListener(ShowPreviousCar);
        }

        private void OnDisable()
        {
            _shopPanel.ItemView -= OnItemView;

            _buyButton.Click -= OnBuyButtonClick;
            _nextButtonCar.onClick.RemoveListener(ShowNextCar);
            _backButtonCar.onClick.RemoveListener(ShowPreviousCar);
        }

        public void Initialize(IDataProvider dataProvider, PlayerMoney wallet, OpenSkinsChecker openSkinsChecker,
            SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
        {
            _wallet = wallet;
            _openSkinsChecker = openSkinsChecker;
            _selectedSkinChecker = selectedSkinChecker;
            _skinSelector = skinSelector;
            _skinUnlocker = skinUnlocker;

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
                _selectedSkinChecker.Visit(_previewedItem.Item);

                if (_selectedSkinChecker.IsSelected)
                {
                    HideBuyButton();
                    return;
                }

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

                _dataProvider.Save();
            }
        }

        private void OnSelectionButtonClick()
        {
            //выбор машины
            
            SelectSkin();

            _dataProvider.Save();
        }

        private void OnCharacterSkinsButtonClick()
        {
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void ShowNextCar()
        {
            contentItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();

            _currentCarIndex++;
            
            if (_currentCarIndex >= contentItems.Count)
            {
                _currentCarIndex = 0;
            }
            
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void ShowPreviousCar()
        {
            contentItems = _contentItems.CharacterSkinItems.Cast<ShopItem>().ToList();
            
            _currentCarIndex--;
            if (_currentCarIndex < 0)
            {
                _currentCarIndex = contentItems.Count - 1;
            }
            
            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>(), _currentCarIndex);
        }

        private void SelectSkin()
        {
            _skinSelector.Visit(_previewedItem.Item);
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