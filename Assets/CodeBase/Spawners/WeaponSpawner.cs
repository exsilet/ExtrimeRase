using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;
using Weapons;

namespace Spawners
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private float _respawnTime;
        
        private System.Random _random = new System.Random();

        private void Start()
        {
            if (CheckSpawnValidity())
            {
                SpawnWeapons();
            }
        }

        public void RespawnWeapon(Transform spawnPoint, float delay)
        {
            StartCoroutine(RespawnWeaponCoroutine(spawnPoint, delay));
        }

        private bool CheckSpawnValidity()
        {
            return _weapons.Count > 1;
        }

        private void SpawnWeapons()
        {
            int[] weaponIndexes = new int[_spawnPoint.Length];

            for (int i = 0; i < _spawnPoint.Length; i++)
            {
                weaponIndexes[i] = -1;
            }

            for (int i = 0; i < _spawnPoint.Length; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = _random.Next(_weapons.Count);
                }
                while ((i > 0 && randomIndex == weaponIndexes[i - 1]) || 
                       (i < _spawnPoint.Length - 1 && randomIndex == weaponIndexes[i + 1]));

                weaponIndexes[i] = randomIndex;
                Transform spawnPoint = _spawnPoint[i];
                GameObject weapon = Instantiate(_weapons[randomIndex].WeaponObject, spawnPoint.transform);
                weapon.GetComponent<ItemPickUp>().SetSpawner(this, spawnPoint, _respawnTime);
            }
        }

        private IEnumerator RespawnWeaponCoroutine(Transform spawnPoint, float delay)
        {
            yield return new WaitForSeconds(delay);

            int randomIndex;
            do
            {
                randomIndex = _random.Next(_weapons.Count);
            }
            while (NearbyHasSameWeapon(spawnPoint.position, _weapons[randomIndex].WeaponObject));

            GameObject weapon = Instantiate(_weapons[randomIndex].WeaponObject, spawnPoint.transform);
            weapon.GetComponent<ItemPickUp>().SetSpawner(this, spawnPoint, _respawnTime);
        }

        private bool NearbyHasSameWeapon(Vector3 position, GameObject weaponPrefab)
        {
            Collider[] colliders = Physics.OverlapSphere(position, 3.0f);
            
            foreach (Collider collider in colliders)
            {
                ItemPickUp weapon = collider.GetComponent<ItemPickUp>();
                if (weapon != null && weapon.gameObject.name == weaponPrefab.name + "(Clone)")
                {
                    return true;
                }
            }
            return false;
        }
    }
}