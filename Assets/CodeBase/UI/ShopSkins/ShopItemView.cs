using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameCar;
        [SerializeField] private TMP_Text _speedCar;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _protection;
        [SerializeField] private TMP_Text _weaponPower;
        [SerializeField] private Image _lockImage;
        [SerializeField] private IntValueView _priceView;
        [SerializeField] private Image _iconBye;

        [SerializeField] private Slider _speedSlider;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _protectionSlider;
        [SerializeField] private Slider _weaponPowerSlider;

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

        public bool IsLock { get; private set; }

        public int Price => Item.Price;
        public GameObject Model => Item.Model;

        public void Initialize(ShopItem item)
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

            _priceView.Show(Price);
        }

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.SetActive(IsLock);
            _priceView.Show(Price);
            _iconBye.gameObject.SetActive(false);
        }

        public void Unlock()
        {
            IsLock = false;
            _lockImage.gameObject.SetActive(IsLock);
            _priceView.Hide();
            _iconBye.gameObject.SetActive(true);
        }

        // Метод для нормализации значений
        private float NormalizeValue(float currentValue, float minValue, float maxValue)
        {
            return (currentValue - minValue) / (maxValue - minValue);
        }
    }
}