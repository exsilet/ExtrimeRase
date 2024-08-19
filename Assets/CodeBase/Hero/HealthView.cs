using UnityEngine;
using UnityEngine.UI;

namespace Hero
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        
        private HeroHealth _heroHealth;

        public void Initialized(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            
            _iconImage.fillAmount = 0;
            _heroHealth.HealthChanged += OnValueChanged;
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= OnValueChanged;

        private void OnValueChanged(float value, float maxValue) => 
            _iconImage.fillAmount = (float)value / maxValue;
    }
}