using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hero
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHp;

        private float _currentHp;
        private bool _shield = false;

        public event UnityAction<float, float> HealthChanged;
        public event UnityAction<HeroHealth> Died;

        public bool Shield => _shield;

        public float Current
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public float Max
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        private void Start()
        {
            _currentHp = _maxHp;
            HealthChanged?.Invoke(_currentHp, _maxHp);
        }

        public void ActivationShield(float second)
        {
            StartCoroutine(TimeShield(second));

            if (_shield == false) 
                StopCoroutine(TimeShield(second));
        }

        public void ResetHealth()
        {
            _currentHp = _maxHp;
            HealthChanged?.Invoke(_currentHp, _maxHp);
        }

        public void TakeDamage(int damage)
        {
            if (_shield == false) 
                _currentHp -= damage;

            HealthChanged?.Invoke(_currentHp, _maxHp);

            if (_currentHp <= 0)
                Die();
        }

        private IEnumerator TimeShield(float second)
        {
            _shield = true;
            
            yield return new WaitForSeconds(second);
            _shield = false;
        }

        private void Die()
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}