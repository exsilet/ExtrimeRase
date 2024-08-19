using UI.ShopSkins;

namespace UI.Visitor
{
    public interface IShopItemVisitor
    {
        void Visit(ShopItem shopItem);
        void Visit(CharacterSkinItem characterSkinItem);
    }
}