using UnityEngine;
using Weapons;

namespace Hero
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out DeadlyArea zone))
            {
                _health.TakeDamage(zone.DamageZone);
            }
        }
    }
}