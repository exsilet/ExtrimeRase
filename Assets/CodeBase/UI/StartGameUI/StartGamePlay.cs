using SaveData;
using UI.ShopSkins;
using UI.Visitor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StartGameUI
{
    public class StartGamePlay : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private SkinSelector _skinSelector;
        private IDataProvider _dataProvider;
        private ShopItem _shopItem;

        public void Initialize(SkinSelector skinsSelector, IDataProvider dataProvider)
        {
            _skinSelector = skinsSelector;
            _dataProvider = dataProvider;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnSelectionButtonClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnSelectionButtonClick);
        }

        public void CurrentCar(ShopItem shopItem)
        {
            _shopItem = shopItem;
        }
        
        private void OnSelectionButtonClick()
        {
            SelectSkin(_shopItem);

            _dataProvider.Save();
        }
        
        private void SelectSkin(ShopItem shopItem)
        {
            _skinSelector.Visit(shopItem);
        }
    }
}