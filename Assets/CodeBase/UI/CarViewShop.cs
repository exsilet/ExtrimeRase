using SaveData;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CarViewShop : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Slider _currentSpeed;
        [SerializeField] private Slider _currentPower;
        [SerializeField] private Slider _currentCoin;

        public void Initialize(CarStaticData data, int price, SaveLoadService saveLoadService)
        {
            
        }
    }
}