using UI.ShopSkins;
using UI.UpgradeSkins;
using UI.Visitor;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "UpgradeItemViewFactory", menuName = "Shop/UpgradeItemViewFactory")]
    public class UpgradeItemViewFactory : ScriptableObject
    {
        [SerializeField] private UpgradeItemView _carItemViewPrefab;
        
        public UpgradeItemView Get(ShopItem data, Transform parent)
        {
            UpgradeItemViewVisitor visitor = new UpgradeItemViewVisitor(_carItemViewPrefab);
            visitor.Visit(data);

            UpgradeItemView instance = Instantiate(visitor.Prefab, parent);
            instance.Initialize(data, data.LvlCar);

            return instance;
        }

        private class UpgradeItemViewVisitor : IShopItemVisitor
        {
            private UpgradeItemView _carItemViewPrefab;
            
            public UpgradeItemViewVisitor(UpgradeItemView carItemViewPrefab)
            {
                _carItemViewPrefab = carItemViewPrefab;
            }

            public UpgradeItemView Prefab { get; private set; }

            public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

            public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _carItemViewPrefab;
        }
    }
}