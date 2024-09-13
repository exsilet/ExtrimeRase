using System;
using SO;

namespace Tracker
{
    [Serializable]
    public class RacerCheckpointData
    {
        public HeroesData HeroesData;
        public LapCounter lapCounter;
        public int lastCheckpoint = 0;
        public float distanceToNextCheckpoint = 0;
    }
}