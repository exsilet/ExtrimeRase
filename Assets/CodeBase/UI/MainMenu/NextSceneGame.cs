using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class NextSceneGame : MonoBehaviour
    {
        [SerializeField] private Button _nextGameSceneButton;

        private string _nameSceneGame;
        private int _nextSceneIndex = 5;
        
        private void Start()
        {
            _nextGameSceneButton.onClick.AddListener(ReturnScene);
        }

        private void OnDestroy()
        {
            _nextGameSceneButton.onClick.RemoveListener(ReturnScene);
        }

        private void ReturnScene()
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }
}