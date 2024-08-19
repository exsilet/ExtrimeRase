using System;
using Enemy;
using Hero;
using UnityEngine;

namespace Weapons
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _detectRange;

        private Transform _target;

        public void Initialized(Transform target)
        {
            _target = target;
        }

        private void Start()
        {
            if (_target == null)
            {
                FindTarget();
            }
        }

        private void Update()
        {
            if (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                
                transform.Translate(transform.forward * _speed * Time.deltaTime);
            }
        }

        private void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectRange);
            float closestDistance = float.MaxValue;

            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.TryGetComponent(out EnemyHealth enemy))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        _target = collider.transform;
                    }
                }
            }
        }

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