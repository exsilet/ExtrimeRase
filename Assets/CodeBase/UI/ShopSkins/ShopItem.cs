﻿using UnityEngine;

namespace UI.ShopSkins
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public GameObject Model { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField, Range(0, 500000)] public int Price { get; private set; }
    }
}