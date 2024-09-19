using UI.ShopSkins;

namespace UI.Visitor
{
    public interface IUpgradeItemViewVisitor
    {
        void Visit(ShopItem shopItem, int lvlCar);
        void Visit(CharacterSkinItem characterSkinItem, int lvlCar);
    }
}