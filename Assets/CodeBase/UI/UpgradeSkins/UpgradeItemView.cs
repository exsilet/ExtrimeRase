using System;
using Common;
using TMPro;
using UI.ShopSkins;
using UnityEngine;

namespace UI.UpgradeSkins
{
    public class UpgradeItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameCar;
        [SerializeField] private TMP_Text _speedCar;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _protection;
        [SerializeField] private TMP_Text _weaponPower;
        [SerializeField] private TMP_Text _upgradeLvlToLvl;
        [SerializeField] private Transform _maxUpgrade;
        [SerializeField] private IntValueView _priceView;

        private readonly int _maxLvlUpgrade = 5;

        public ShopItem Item { get; private set; }
        public int Price => Item.Price;
        
        public GameObject Model => Item.Model;

        public void Initialize(ShopItem item, int levelCar)
        {
            Item = item;

            _nameCar.text = item.NameCar;
            _speedCar.text = item.Speed.ToString();
            _protection.text = item.Protection.ToString();
            _health.text = item.Health.ToString();
            _weaponPower.text = item.WeaponPower.ToString();

            if (levelCar < _maxLvlUpgrade - 1)
            {
                _upgradeLvlToLvl.text = $"LvL {levelCar + 1} -> LvL {levelCar + 2}";
                _maxUpgrade.gameObject.SetActive(false);
                _priceView.Show(Price);
            }
            else
            {
                _upgradeLvlToLvl.text = $"LVL {_maxLvlUpgrade}";
                _priceView.Hide(); 
                _maxUpgrade.gameObject.SetActive(true);
            }
        }
    }
}