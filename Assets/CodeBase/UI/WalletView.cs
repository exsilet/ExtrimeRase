using System.Globalization;
using Hero;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        private PlayerMoney _wallet;

        public void Initialize(PlayerMoney wallet)
        {
            _wallet = wallet;

            UpdateValue(_wallet.GetCurrentCoins());

            _wallet.CoinsChanged += UpdateValue;
        }

        private void OnDestroy() => _wallet.CoinsChanged -= UpdateValue;

        private void UpdateValue(int value) => _value.text = value.ToString("N0", CultureInfo.InvariantCulture);
    }
}