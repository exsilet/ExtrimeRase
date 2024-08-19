using UnityEngine;

namespace SO
{
    public class CarStaticData : ScriptableObject
    {
        public Sprite UIIcon;
        public GameObject PrefabSelect;
        public int StartPrice;
        public bool IsBuy;
        
        [Range(0f, 5f)] public float Speed;
    }
}