using UnityEngine;

namespace UI.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _upgrade;
        [SerializeField] private AudioSource _audioSource;

        private float _volume;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _volume = _audioSource.volume;
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