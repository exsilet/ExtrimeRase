using UnityEngine;

namespace Tracker
{
    public class LapCounter : MonoBehaviour
    {
        private int _currentLap = 0;
        public int CurrentLap => _currentLap;
        
        public void IncrementLap()
        {
            _currentLap++;
        }
    }
}