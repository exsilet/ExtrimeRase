using UI.ShopSkins;
using UI.Visitor;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
    public class ShopItemViewFactory : ScriptableObject
    {
        [SerializeField] private ShopItemView _characterSkinItemPrefab;
    
        private int _price;

        public ShopItemView Get(ShopItem data, Transform parent)
        {
            ShopItemVisitor visitor = new ShopItemVisitor(_characterSkinItemPrefab);
            visitor.Visit(data);

            ShopItemView instance = Instantiate(visitor.Prefab, parent);
            instance.Initialize(data);

            return instance;
        }

        private class ShopItemVisitor : IShopItemVisitor
        {
            private ShopItemView _characterSkinItemPrefab;
            public ShopItemVisitor(ShopItemView characterSkinItemPrefab)
            {
                _characterSkinItemPrefab = characterSkinItemPrefab;
            }

            public ShopItemView Prefab { get; private set; }

            public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

            public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _characterSkinItemPrefab;
        }
    }
}