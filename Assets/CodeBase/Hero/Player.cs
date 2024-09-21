using DefaultNamespace;
using SO;
using UnityEngine;

namespace Hero
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private HeroesData _heroes;

        //private const string Attack = "Fire1";

        public HeroesData Heroes => _heroes;
        
        private void Update()
        {
            if (Application.isMobilePlatform)
            {
                
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                {
                    _inventory.UsedWeapon();
                }
            }
        }

        public void UseAttack()
        {
            _inventory.UsedWeapon();
        }
    }
}