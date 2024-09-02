using UnityEngine;

namespace UI.ShopSkins
{
    public class UpgradeCar : MonoBehaviour
    {
        [SerializeField] private CharacterSkinItem carSkinItem;

        public CharacterSkinItem CarSkinItem => carSkinItem;
    }
}