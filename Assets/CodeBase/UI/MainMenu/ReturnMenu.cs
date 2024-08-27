using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ReturnMenu : MonoBehaviour
    {
        private const string Menu = "Главное меню";
        
        [SerializeField] private Button _returnMenu;
        
        private void Start()
        {
            _returnMenu.onClick.AddListener(Return);
        }

        private void OnDestroy()
        {
            _returnMenu.onClick.RemoveListener(Return);
        }

        private void Return()
        {
            SceneManager.LoadScene(Menu);
        }
    }
}