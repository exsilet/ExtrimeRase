using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class NextScene : MonoBehaviour
    {
        [SerializeField] private Button[] _nextSceneButton;

        private void Start()
        {
            foreach (Button button in _nextSceneButton)
            {
                button.onClick.AddListener(ReturnScene);
            }
        }

        private void OnDestroy()
        {
            foreach (Button button in _nextSceneButton)
            {
                button.onClick.RemoveListener(ReturnScene);
            }
        }

        private void ReturnScene()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }
}