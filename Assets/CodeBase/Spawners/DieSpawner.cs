using System.Collections.Generic;
using SO;
using Tracker;
using UnityEngine;

namespace Spawners
{
    public class DieSpawner : MonoBehaviour
    {
        [SerializeField] private List<Checkpoint> _checkpoints;

        private List<HeroesData> _heroes = new();

        public void AddHeroes(HeroesData data)
        {
            _heroes.Add(data);
        }

        public void Respawn()
        {
            
        }
    }
}