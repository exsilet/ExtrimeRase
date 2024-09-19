using SaveData;
using UI.ShopSkins;

namespace UI.Visitor
{
    public class SkinUpdater : IUpgradeItemViewVisitor
    {
        private IPersistentData _persistentData;

        public SkinUpdater(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem, int lvlCar) => Visit((dynamic)shopItem, lvlCar);

        public void Visit(CharacterSkinItem characterSkinItem, int lvlCar) => 
            _persistentData.DataBase.AddNewCar(characterSkinItem.SkinType, lvlCar);
    }
}