using System;
using Common;
using TMPro;
using UI.ShopSkins;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private Slider _speedSlider;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _protectionSlider;
        [SerializeField] private Slider _weaponPowerSlider;

        private readonly int _maxLvlUpgrade = 5;

        // Минимальные и максимальные значения для нормализации
        private readonly float _minSpeed = 10f;
        private readonly float _maxSpeed = 142f;

        private readonly float _minHealth = 10f;
        private readonly float _maxHealth = 126f;

        private readonly float _minProtection = 10f;
        private readonly float _maxProtection = 136f;

        private readonly float _minWeaponPower = 5f;
        private readonly float _maxWeaponPower = 118f;

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

            // Установка значений слайдеров на основе нормализации
            _speedSlider.value = NormalizeValue(item.Speed, _minSpeed, _maxSpeed);
            _healthSlider.value = NormalizeValue(item.Health, _minHealth, _maxHealth);
            _protectionSlider.value = NormalizeValue(item.Protection, _minProtection, _maxProtection);
            _weaponPowerSlider.value = NormalizeValue(item.WeaponPower, _minWeaponPower, _maxWeaponPower);

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

        // Метод для нормализации значений
        private float NormalizeValue(float currentValue, float minValue, float maxValue)
        {
            return (currentValue - minValue) / (maxValue - minValue);
        }
    }
}