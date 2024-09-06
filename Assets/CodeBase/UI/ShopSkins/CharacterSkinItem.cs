using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "CharacterSkinItem", menuName = "Shop/CharacterSkinItem")]
    public class CharacterSkinItem : ShopItem
    {
        [field: SerializeField] public CarSkins SkinType { get; private set; }
    }
}