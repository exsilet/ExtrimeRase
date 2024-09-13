using ArcadeVP;
using SO;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        //[SerializeField] private ArcadeAiVehicleController _controller;
        
        private string _name;
        private HeroesData _enemy;

        public HeroesData HeroesEnemyData
        {
            get => _enemy;
            set => _enemy = value;
        }
    }
}