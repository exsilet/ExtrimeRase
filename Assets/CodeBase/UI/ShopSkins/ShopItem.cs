using UnityEngine;

namespace UI.ShopSkins
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public string NameCar { get; private set; }
        [field: SerializeField] public int Speed { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Protection { get; private set; }
        [field: SerializeField] public int WeaponPower { get; private set; }
        [field: SerializeField] public GameObject Model { get; private set; }
        [field: SerializeField, Range(0, 4)] public int LvlCar { get; private set; }
        [field: SerializeField, Range(0, 500000)] public int Price { get; private set; }
    }
}