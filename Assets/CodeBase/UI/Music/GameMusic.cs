using UnityEngine;

namespace UI.Music
{
    public class GameMusic : MonoBehaviour
    {
        private const string MusicParameter = "Music";
        
        [SerializeField] private AudioSource _audioSource;

        private float _volume;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MusicParameter))
            {
                _audioSource.volume = 1;
            }
            else
                _audioSource.volume = PlayerPrefs.GetFloat(MusicParameter);

        }
    }
}