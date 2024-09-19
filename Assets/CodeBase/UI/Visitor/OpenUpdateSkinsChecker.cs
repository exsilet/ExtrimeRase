using System.Collections.Generic;
using System.Linq;
using SaveData;
using UI.ShopSkins;

namespace UI.Visitor
{
    public class OpenUpdateSkinsChecker : IUpgradeItemViewVisitor
    {
        private IPersistentData _persistentData;

        public bool IsOpened { get; private set; }

        public int IndexModel { get; private set; }
        public OpenUpdateSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem, int lvlCar) => Visit((dynamic)shopItem, lvlCar);

        public void Visit(CharacterSkinItem characterSkinItem, int lvlCar)
        {
            IsOpened = _persistentData.DataBase.OpenUpdateCharacterSkins
                .Where(t => t.CarSkins == characterSkinItem.SkinType).Any(t => t.LvlCarUpgrade == lvlCar);

            IndexModel = (int)characterSkinItem.SkinType;
        }
    }
}