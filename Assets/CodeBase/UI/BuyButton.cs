using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyButton : MonoBehaviour
    {
        public event Action Click;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;

        private bool _isLock;

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public void UpdateText(int price) => _text.text = price.ToString("N0", CultureInfo.InvariantCulture);

        public void Lock()
        {
            _isLock = true;
            _text.color = _lockColor;
        }

        public void Unlock()
        {
            _isLock = false;
            _text.color = _unlockColor;
        }

        private void OnButtonClick()
        {
            if (_isLock)
            {
                return;
            }

            Click?.Invoke();
        }
    }
}