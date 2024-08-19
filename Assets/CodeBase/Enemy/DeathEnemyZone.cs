using UnityEngine;
using Weapons;

namespace Enemy
{
    public class DeathEnemyZone : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out DeadlyArea zone))
            {
                _health.TakeDamage(zone.DamageZone);
            }
        }
    }
}