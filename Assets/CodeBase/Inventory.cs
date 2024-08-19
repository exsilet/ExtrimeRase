using System;
using System.Collections.Generic;
using Hero;
using SO;
using UI;
using UnityEngine;
using Weapons;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _boomPoint;
        [SerializeField] private HeroHealth _heroHealth;
        
        private WeaponView _weaponView;
        private List<Weapon> _weapons = new List<Weapon>();
        private Weapon _weapon;

        public void Initialized(WeaponView weaponView)
        {
            _weaponView = weaponView;
        }
        
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

        public void UsedWeapon()
        {
            if (_weapon == null) return;

            ChoseWeapon();
            ClearWeapon();
        }

        private void ChoseWeapon()
        {
            switch (_weapon.WeaponType)
            {
                case WeaponType.Rocket:
                    _weapon.Shoot(_shootPoint);
                    break;
                case WeaponType.Oil:
                    _weapon.Shoot(_boomPoint);
                    break;
                case WeaponType.Mine:
                    _weapon.Shoot(_boomPoint);
                    break;
                case WeaponType.Protection:
                    _heroHealth.ActivationShield(_weapon.SecondTime);
                    break;
                case WeaponType.Acceleration:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void NewWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapons.Add(weapon);
            _weaponView.Render(weapon);
        }

        private void ClearWeapon()
        {
            _weapons.RemoveAt(0);
            _weapon = null;
            _weaponView.UsedWeapon();
        }
    }
}