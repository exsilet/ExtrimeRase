using ArcadeVP;
using SO;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ArcadeAiVehicleController _controller;
        
        private string _name;
        private HeroesData _enemy;
        
        public void Initialized(string name, HeroesData enemy)
        {
            _name = name;
            _enemy = enemy;
        }
    }
}