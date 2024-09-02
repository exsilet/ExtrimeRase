using System;
using UI.ShopSkins;

namespace SaveData
{
    [Serializable]
    public class SlotCarData
    {
        public CarSkins CarSkins;
        public int LvlCarUpgrade;

        public SlotCarData(CarSkins carSkins, int lvlCarUpgrade)
        {
            CarSkins = carSkins;
            LvlCarUpgrade = lvlCarUpgrade;
        }
    }
}