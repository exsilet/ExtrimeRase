using UnityEngine;

namespace Tracker
{
    public class Checkpoint : MonoBehaviour
    {
        private RacePosition _racePosition;
        private int _checkpointIndex;

        public void Initialized(RacePosition racePosition, int index)
        {
            _racePosition = racePosition;
            _checkpointIndex = index;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out LapCounter lapCounter))
            {
                if (lapCounter != null)
                {
                    _racePosition.RacerPassedCheckpoint(lapCounter, _checkpointIndex);
                }
            }
        }
    }
}