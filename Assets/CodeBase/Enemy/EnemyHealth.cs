using System.Collections;
using Hero;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _maxHp;

        private float _currentHealth;

        private bool _shield = false;
        public bool ShieldEnemy => _shield;

        public event UnityAction<EnemyHealth> DiedEnemy;

        public float Current
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public float Max
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        private void Start() => _currentHealth = _maxHp;
        public void ResetHealth() => _currentHealth = _maxHp;

        public void TakeDamage(int damage)
        {
            if (_shield == false)
                _currentHealth -= damage;

            if (_currentHealth <= 0)
                Die();
        }

        public void ActivationShield(float second)
        {
            StartCoroutine(TimeShield(second));

            if (_shield == false)
                StopCoroutine(TimeShield(second));
        }

        private IEnumerator TimeShield(float second)
        {
            _shield = true;

            yield return new WaitForSeconds(second);
            _shield = false;
        }

        private void Die()
        {
            DiedEnemy?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}