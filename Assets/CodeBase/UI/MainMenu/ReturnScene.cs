using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ReturnScene : MonoBehaviour
    {
        [SerializeField] private Button _returnSceneButton;

        private void Start()
        {
            _returnSceneButton.onClick.AddListener(Return);
        }

        private void OnDestroy()
        {
            _returnSceneButton.onClick.RemoveListener(Return);
        }

        private void Return()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index - 1);
        }
    }
}