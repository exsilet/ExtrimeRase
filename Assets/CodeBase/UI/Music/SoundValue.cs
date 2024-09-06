using UnityEngine;
using UnityEngine.UI;

namespace UI.Music
{
    public class SoundValue : MonoBehaviour
    {
        private const string SoundVolume = "SoundVolume";
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Slider _slider;

        private float _volume;
        
        private void Start()
        {
            if (PlayerPrefs.HasKey(SoundVolume))
            {
                _slider.value = PlayerPrefs.GetFloat(SoundVolume);
            }
            else
                _slider.value = 1;

            _audioSource.volume = _slider.value;
        }
        
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolumeMusic);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolumeMusic);
        }

        private void ChangeVolumeMusic(float value)
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat(SoundVolume, value);
        }
    }
}