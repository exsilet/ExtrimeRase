using System;
using System.Collections.Generic;
using System.Linq;
using UI.ShopSkins;
using UnityEngine;

namespace UI.UpgradeSkins
{
    [CreateAssetMenu(fileName = "UpgradeContent", menuName = "Shop/UpgradeContent")]
    public class UpgradeContent : ScriptableObject
    {
        [SerializeField] private List<CharacterSkinItem> _characterSkinItems;

        public IEnumerable<CharacterSkinItem> CharacterSkinItems => _characterSkinItems;

        private void OnValidate()
        {
            var characterSkinsDuplicates = _characterSkinItems.GroupBy(item => item.SkinType)
                .Where(array => array.Count() > 5);

            if (characterSkinsDuplicates.Count() > 0)
                throw new InvalidOperationException(nameof(_characterSkinItems));
        }
    }
}