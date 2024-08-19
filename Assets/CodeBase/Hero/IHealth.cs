using Enemy;
using UnityEngine.Events;

namespace Hero
{
    public interface IHealth
    {
        public event UnityAction<EnemyHealth> DiedEnemy;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(int damage);
    }
}