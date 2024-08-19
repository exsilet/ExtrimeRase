using SaveData;
using UI.ShopSkins;

namespace UI.Visitor
{
    public class SkinUnlocker : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem) 
            => _persistentData.DataBase.OpenCharacterSkin(characterSkinItem.SkinType);
    }
}