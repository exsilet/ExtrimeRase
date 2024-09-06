using UI.ShopSkins;
using UnityEngine;

namespace UI.UpgradeSkins
{
    public class CharacterSkin : MonoBehaviour
    {
        [SerializeField] private CharacterSkinItem carSkinItem;

        public CharacterSkinItem CarSkinItem => carSkinItem;
    }
}