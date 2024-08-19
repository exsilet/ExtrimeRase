using System;
using System.Collections.Generic;
using SO;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyInventory : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _boomPoint;
        [SerializeField] private EnemyHealth _enemy;
        
        private List<Weapon> _weapons = new List<Weapon>();
        private Weapon _weapon;

        public void Initialized(Weapon weapon)
        {
            if (_weapon == null)
            {
                NewWeapon(weapon);
            }
            else
            {
                ClearWeapon();
                NewWeapon(weapon);
            }
        }

        public void UseFontWeapon(Transform target)
        {
            if (_weapon == null) return;
            
            ChoseFontWeapon(target);
            ClearWeapon();
        }

        public void UseBehindWeapon()
        {
            if (_weapon == null) return;
            
            BehindWeapons();
            ClearWeapon();
        }

        private void BehindWeapons()
        {
            if (_weapon.WeaponType == WeaponType.Rocket)
                return;
            
            switch (_weapon.WeaponType)
            {
                case WeaponType.Oil:
                    _weapon.Shoot(_boomPoint);
                    break;
                case WeaponType.Mine:
                    _weapon.Shoot(_boomPoint);
                    break;
                case WeaponType.Protection:
                    _enemy.ActivationShield(_weapon.SecondTime);
                    break;
                case WeaponType.Acceleration:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void ChoseFontWeapon(Transform target)
        {
            if (_weapon.WeaponType == WeaponType.Rocket)
            {
                _weapon.Shoot(_shootPoint, target);
            }
        }

        private void NewWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapons.Add(weapon);
        }

        private void ClearWeapon()
        {
            _weapons.RemoveAt(0);
            _weapon = null;
        }
    }
}