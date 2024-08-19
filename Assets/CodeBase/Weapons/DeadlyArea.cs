using UnityEngine;

namespace Weapons
{
    public class DeadlyArea : MonoBehaviour
    {
        [SerializeField] private int _damage;
        public int DamageZone => _damage;
    }
}