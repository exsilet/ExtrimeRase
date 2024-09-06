using SaveData;
using UI.ShopSkins;

namespace UI.Visitor
{
    public class SkinSelector : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem) 
            => _persistentData.DataBase.SelectedCarSkin = characterSkinItem.SkinType;
    }
}