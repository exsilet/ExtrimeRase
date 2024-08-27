using System;
using System.Collections.Generic;
using System.Linq;
using UI.Visitor;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.ShopSkins
{
    public class ShopPanel : MonoBehaviour
    {
        public event Action<ShopItemView> ItemView;

        private List<ShopItemView> _shopItems = new List<ShopItemView>();

        [SerializeField] private Transform _itemsParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

        private OpenSkinsChecker _openSkinsChecker;
        private SelectedSkinChecker _selectedSkinChecker;
        private int _currentCarIndex = 0;

        public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
        {
            _openSkinsChecker = openSkinsChecker;
            _selectedSkinChecker = selectedSkinChecker;
        }

        public void Show(IEnumerable<ShopItem> items, int index)
        {
            Clear();

            IList<ShopItem> item = items.AsReadOnlyList();
            
            ShowCarCurrent(item, index);

            //Sort();
        }

        private void ShowCarCurrent(IList<ShopItem> items, int index)
        {
            _currentCarIndex = index;
            
            for (int i = 0; i < items.Count; i++)
            {
                if (i == _currentCarIndex)
                {
                    ShopItemView spawnedItem = _shopItemViewFactory.Get(items[i], _itemsParent);

                    ItemView?.Invoke(spawnedItem);

                    _openSkinsChecker.Visit(spawnedItem.Item);

                    if (_openSkinsChecker.IsOpened)
                    {
                        _selectedSkinChecker.Visit(spawnedItem.Item);

                        if (_selectedSkinChecker.IsSelected)
                        {
                            //spawnedItem.Select();
                            ItemView?.Invoke(spawnedItem);
                        }

                        spawnedItem.Unlock();
                    }
                    else
                    {
                        spawnedItem.Lock();
                    }
                    
                    _shopItems.Add(spawnedItem);
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

        private void Sort()
        {
            _shopItems = _shopItems
                .OrderBy(item => item.IsLock)
                .ThenByDescending(item => item.Price)
                .ToList();

            for (int i = 0; i < _shopItems.Count; i++)
                _shopItems[i].transform.SetSiblingIndex(i);
        }

        private void OnItemViewClick(ShopItemView itemView)
        {
            Debug.Log(" app car " + itemView);
            
            ItemView?.Invoke(itemView);
        }

        private void Clear()
        {
            foreach (ShopItemView item in _shopItems)
            {
                item.Click -= OnItemViewClick;
                Destroy(item.gameObject);
            }

            _shopItems.Clear();
        }
    }
}