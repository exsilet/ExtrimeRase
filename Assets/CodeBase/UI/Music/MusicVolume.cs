using UnityEngine;
using UnityEngine.UI;

namespace UI.Music
{
    public class MusicVolume : MonoBehaviour
    {
        private const string MusicParameter = "Music";
        
        [SerializeField] private AudioSource _audioSourceMusic;
        [SerializeField] private Slider _slider;
        
        private float _volume;
        
        private void Start()
        {
            if (PlayerPrefs.HasKey(MusicParameter))
            {
                _slider.value = PlayerPrefs.GetFloat(MusicParameter);
            }
            else
                _slider.value = 0.5f;

            _audioSourceMusic.volume = _slider.value;
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
            _audioSourceMusic.volume = value;
            PlayerPrefs.SetFloat(MusicParameter, value);
        }
    }
}