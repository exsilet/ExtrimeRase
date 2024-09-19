using UI.ShopSkins;
using UnityEngine;

namespace UI.UpgradeSkins
{
    public class CharacterSkin : MonoBehaviour
    {
        [SerializeField] private CharacterSkinItem _carSkinItem;

        public CharacterSkinItem CarSkinItem => _carSkinItem;
        public ShopItem ShopItems => _carSkinItem;
    }
}