using UnityEngine;

namespace UI.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Transform _mainMenuPanel;
        [SerializeField] private Transform _shopPanel;
        [SerializeField] private Transform _autoServicePanel;
        
        public void GoToShop()
        {
            _mainMenuPanel.gameObject.SetActive(false);
            _shopPanel.gameObject.SetActive(true);
            _autoServicePanel.gameObject.SetActive(false);
        }
        
        public void GoToAutoService()
        {
            _mainMenuPanel.gameObject.SetActive(false);
            _shopPanel.gameObject.SetActive(false);
            _autoServicePanel.gameObject.SetActive(true);
        }
        
        public void BackToMainMenu()
        {
            _mainMenuPanel.gameObject.SetActive(true);
            _shopPanel.gameObject.SetActive(false);
            _autoServicePanel.gameObject.SetActive(false);
        }
    }
}

