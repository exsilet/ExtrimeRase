﻿using System.Linq;
using SaveData;
using UI.ShopSkins;

namespace UI.Visitor
{
    public class OpenSkinsChecker : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public bool IsOpened { get; private set; }

        public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem) 
            => IsOpened = _persistentData.DataBase.OpenCharacterSkins.Contains(characterSkinItem.SkinType);
    }
}