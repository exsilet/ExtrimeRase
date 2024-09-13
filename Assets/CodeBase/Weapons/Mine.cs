using Enemies;
using Hero;
using UnityEngine;

namespace Weapons
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private int _damage;
        
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }
            
            if (collision.gameObject.TryGetComponent(out HeroHealth player))
            {
                player.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}