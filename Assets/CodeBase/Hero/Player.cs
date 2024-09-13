using DefaultNamespace;
using SO;
using UnityEngine;

namespace Hero
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private HeroesData _heroes;

        private const string Attack = "Fire1";

        public HeroesData Heroes => _heroes;
        
        private void Update()
        {
            if (Input.GetButtonUp(Attack))
            {
                _inventory.UsedWeapon();
            }
        }
    }
}