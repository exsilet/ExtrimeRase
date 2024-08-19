using System.Collections.Generic;
using ArcadeVP;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class SwapPosition : MonoBehaviour
    {
        private WaypointProgressTracker _waypoint;
        private List<WaypointCircuit> _waypointCircuit = new();

        private int _pontIndex = 0;
        private WaypointCircuit _swapToPoint;

        public WaypointCircuit SwapToPoint => _swapToPoint;

        private void Start()
        {
            //_swapToPoint = _waypointCircuit[Random.Range(0, _waypointCircuit.Count)];
            _waypoint.circuit = _swapToPoint;
        }

        public void Initialized(WaypointProgressTracker waypoint, List<WaypointCircuit> waypointCircuits, int indexSwap)
        {
            _waypoint = waypoint;
            _waypointCircuit.AddRange(waypointCircuits);
            _swapToPoint = _waypointCircuit[indexSwap];
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            // {
            //     _swapToPoint = _waypointCircuit[Random.Range(0, _waypointCircuit.Count)];
            //     _waypoint.circuit = _swapToPoint;
            //
            //     WaypointPosition();
            // }
        }

        private void OnCollisionEnter(Collision other)
        {
            // if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            // {
            //     Debug.Log(" max index " + _waypointCircuit.Count);
            //     
            //     WaypointPosition(enemy.GetComponent<SwapPosition>());
            // }
        }

        private void WaypointPosition(SwapPosition enemy)
        {
            for (int i = 0; i < _waypointCircuit.Count; i++)
            {
                if (enemy.SwapToPoint == _waypointCircuit[i])
                {
                    _pontIndex = i + 1;
                    
                    if (_pontIndex > _waypointCircuit.Count-1)
                    {
                        _pontIndex = 0;
                        _swapToPoint = _waypointCircuit[_pontIndex];
                    }
                    
                    _swapToPoint = _waypointCircuit[_pontIndex];
                    _waypoint.circuit = _swapToPoint;
                }
            }
        }
    }
}