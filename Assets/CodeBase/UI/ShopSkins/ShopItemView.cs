using System;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class ShopItemView : MonoBehaviour
    {
        public event Action<ShopItemView> Click;

        [SerializeField] private TMP_Text _nameCar;
        [SerializeField] private TMP_Text _speedCar;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _protection;
        [SerializeField] private TMP_Text _weaponPower;
        [SerializeField] private Image _lockImage;
        [SerializeField] private IntValueView _priceView;
        [SerializeField] private Image _iconBye;
        
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

            _priceView.Show(Price);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(this);
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

        public ShopItem GetShopItem()
        {
            return Item;
        }
    }
}