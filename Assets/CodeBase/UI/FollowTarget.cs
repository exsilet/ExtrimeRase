using System;
using UnityEngine;

namespace UI
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        
        private Transform _target;

        public void Initialized(Transform target)
        {
            _target = target;
        }
        
        private void Update()
        {
            transform.position = _target.transform.position + _offset;
        }
    }
}