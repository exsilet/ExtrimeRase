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
            visitor.Visit(data, data.LvlCar);

            UpgradeItemView instance = Instantiate(visitor.Prefab, parent);
            instance.Initialize(data, data.LvlCar);

            return instance;
        }

        private class UpgradeItemViewVisitor : IUpgradeItemViewVisitor
        {
            private UpgradeItemView _carItemViewPrefab;
            
            public UpgradeItemViewVisitor(UpgradeItemView carItemViewPrefab)
            {
                _carItemViewPrefab = carItemViewPrefab;
            }

            public UpgradeItemView Prefab { get; private set; }

            public void Visit(ShopItem shopItem, int lvlCar) => Visit((dynamic)shopItem, lvlCar);

            public void Visit(CharacterSkinItem characterSkinItem, int lvlCar)
            {
                Prefab = _carItemViewPrefab;
            }
        }
    }
}