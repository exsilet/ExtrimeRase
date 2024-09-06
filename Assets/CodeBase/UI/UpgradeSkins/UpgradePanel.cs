using System;
using System.Collections.Generic;
using UI.ShopSkins;
using UI.Visitor;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.UpgradeSkins
{
    public class UpgradePanel : MonoBehaviour
    {
        public event Action<UpgradeItemView> ItemView;

        private List<UpgradeItemView> _upgradeItems = new List<UpgradeItemView>();

        [SerializeField] private Transform _itemsParent;
        [SerializeField] private UpgradeItemViewFactory _upgradeItemViewFactory;

        private OpenSkinsChecker _openSkinsChecker;
        
        public void Initialize(OpenSkinsChecker openSkinsChecker)
        {
            _openSkinsChecker = openSkinsChecker;
        }
        
        public void Show(IEnumerable<ShopItem> items, ShopItem shopItem)
        {
            Clear();

            IList<ShopItem> item = items.AsReadOnlyList();
            
            ShowCarCurrent(item, shopItem);
        }

        private void ShowCarCurrent(IList<ShopItem> items, ShopItem shopItem)
        {
            foreach (var item in items)
            {
                if (item == shopItem)
                {
                    UpgradeItemView spawnedItem = _upgradeItemViewFactory.Get(item, _itemsParent);

                    ItemView?.Invoke(spawnedItem);
                    
                    _upgradeItems.Add(spawnedItem);
                }
            }
        }

        // public void Select(ShopItemView itemView)
        // {
        //     foreach (var item in _shopItems)
        //         item.Unselect();
        //
        //     itemView.Select();
        // }

        private void Clear()
        {
            foreach (UpgradeItemView item in _upgradeItems)
            {
                Destroy(item.gameObject);
            }

            _upgradeItems.Clear();
        }
    }
}