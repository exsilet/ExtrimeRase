using DefaultNamespace;
using Enemies;
using SO;
using Spawners;
using UnityEngine;

namespace Weapons
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private GameObject _objectWeapon;

        private WeaponType _weaponType;
        private WeaponSpawner _weaponSpawner;
        private Transform _spawnPoint;
        private float _respawnTime;
        
        private void Start() => 
            _weaponType = _weapon.WeaponType;

        public void SetSpawner(WeaponSpawner weaponSpawner, Transform spawnPoint, float respawnTime)
        {
            _weaponSpawner = weaponSpawner;
            _spawnPoint = spawnPoint;
            _respawnTime = respawnTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Inventory inventory))
            {
                inventory.Initialized(_weapon);
                _objectWeapon.SetActive(false);
                _weaponSpawner.RespawnWeapon(_spawnPoint, _respawnTime);
            }
            
            if (other.gameObject.TryGetComponent(out EnemyInventory enemyInventory))
            {
                enemyInventory.Initialized(_weapon);
                _objectWeapon.SetActive(false);
                _weaponSpawner.RespawnWeapon(_spawnPoint, _respawnTime);
            }
        }
    }
}