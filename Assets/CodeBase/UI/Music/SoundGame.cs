using UnityEngine;

namespace UI.Music
{
    public class SoundGame : MonoBehaviour
    {
        private const string SoundVolume = "SoundVolume";
        
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _upgrade;
        [SerializeField] private AudioSource _audioSource;

        private float _volume;
        
        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _audioSource.volume = 1;
            }
            else
                _audioSource.volume = PlayerPrefs.GetFloat(SoundVolume);
        }

        public void PlayClick()
        {
            _audioSource.PlayOneShot(_click, _volume);
        }

        public void PlayUpgrade()
        {
            _audioSource.PlayOneShot(_upgrade, _volume);
        }
    }
}