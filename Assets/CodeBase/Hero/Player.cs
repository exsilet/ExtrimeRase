using DefaultNamespace;
using UnityEngine;

namespace Hero
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        private const string Attack = "Fire1";
        
        private void Update()
        {
            if (Input.GetButtonUp(Attack))
            {
                _inventory.UsedWeapon();
            }
        }
    }
}