using System;

namespace Tracker
{
    [Serializable]
    public class RacerCheckpointData
    {
        public LapCounter racer;
        public int lastCheckpoint = 0;
        public float distanceToNextCheckpoint = 0;
    }
}