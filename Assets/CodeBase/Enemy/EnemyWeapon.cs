using UnityEngine;

namespace Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private LayerMask _obstaclesLayer;
        
        [SerializeField] private EnemyInventory _inventory;
        
        [SerializeField] private Transform _review;
        [SerializeField] private Transform _reviewBehind;
        
        [SerializeField] private float _targetLostDelay;
        [SerializeField] private float _distancePosition;
        [SerializeField] private float _distanceBehindPosition;
        [SerializeField] private float _viewAngle;

        private Transform _theGoalIsAhead;
        private Transform _theTargetIsBehind;
        private RaycastHit hit;
        private float _const = 2f;
        private float _lastSeenTimeAhead;
        private float _lastSeenTimeBehind;
        private float _distanceToTarget;

        private void FixedUpdate()
        {
            TargetPosition();
            TargetPositionBehind();
            HandleTargetLost();
            
            if (_theGoalIsAhead != null) 
                LaunchFont(_theGoalIsAhead);

            if (_theTargetIsBehind != null) 
                LaunchBehind();
            
            DrawViewState();
            DrawBehindState();
        }

        private void TargetPosition()
        {
            Collider[] enemiesInViewRadius = Physics.OverlapSphere(_review.position, _distancePosition, _playerLayerMask);

            foreach (Collider enemy in enemiesInViewRadius)
            {
                Transform enemyTransform = enemy.transform;
                Vector3 dirToEnemy = (enemyTransform.position - _review.position).normalized;

                if (Vector3.Angle(_review.forward, dirToEnemy) < _viewAngle / _const)
                {
                    float distanceToEnemy = Vector3.Distance(_review.position, enemyTransform.position);

                    if (!Physics.Raycast(_review.position, dirToEnemy, distanceToEnemy, _obstaclesLayer))
                    {
                        _theGoalIsAhead = enemyTransform;
                    }
                }
            }
        }
        
        private void TargetPositionBehind()
        {
            Collider[] enemiesInViewRadius = Physics.OverlapSphere(_reviewBehind.position, _distanceBehindPosition, _playerLayerMask);

            foreach (Collider enemy in enemiesInViewRadius)
            {
                Transform enemyTransform = enemy.transform;
                Vector3 dirToEnemy = (enemyTransform.position - _reviewBehind.position).normalized;

                if (Vector3.Angle(-_reviewBehind.forward, dirToEnemy) < _viewAngle / _const)
                {
                    float distanceToEnemy = Vector3.Distance(_reviewBehind.position, enemyTransform.position);

                    if (!Physics.Raycast(_reviewBehind.position, dirToEnemy, distanceToEnemy, _obstaclesLayer))
                    {
                        _theTargetIsBehind = enemyTransform;
                    }
                }
            }
        }

        private void DrawViewState()
        {
            Vector3 left = _review.position + Quaternion.Euler(new Vector3(0, _viewAngle / _const, 0)) * (_review.forward * _distancePosition);
            Vector3 right = _review.position + Quaternion.Euler(-new Vector3(0, _viewAngle / _const, 0)) * (_review.forward * _distancePosition);
            
            Debug.DrawLine(_review.position, left, Color.blue);
            Debug.DrawLine(_review.position, right, Color.blue);
        }
        
        private void DrawBehindState()
        {
            Vector3 left = _reviewBehind.position + Quaternion.Euler(new Vector3(0, -_viewAngle / _const, 0)) * (-_reviewBehind.forward * _distanceBehindPosition);
            Vector3 right = _reviewBehind.position + Quaternion.Euler(new Vector3(0, _viewAngle / _const, 0)) * (-_reviewBehind.forward * _distanceBehindPosition);
            
            Debug.DrawLine(_reviewBehind.position, left, Color.blue);
            Debug.DrawLine(_reviewBehind.position, right, Color.blue);
        }
        
        private void HandleTargetLost()
        {
            if (_theGoalIsAhead != null)
            {
                _distanceToTarget = Vector3.Distance(_review.position, _theGoalIsAhead.position);
            
                if (_distanceToTarget > _distancePosition)
                {
                    _theGoalIsAhead = null;
                }
            }
        
            if (_theTargetIsBehind != null)
            {
                _distanceToTarget = Vector3.Distance(_reviewBehind.position, _theTargetIsBehind.position);
            
                if (_distanceToTarget > _distanceBehindPosition)
                {
                    _theTargetIsBehind = null;
                }
            }
        }
        
        private void LaunchFont(Transform target) => _inventory.UseFontWeapon(target);

        private void LaunchBehind() => _inventory.UseBehindWeapon();
    }
}