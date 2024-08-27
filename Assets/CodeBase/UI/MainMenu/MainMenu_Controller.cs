using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenu_Controller : MonoBehaviour
    {
        
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _raceSelectionPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _helpPanel;

        // Метод для перехода к панели гонок
        public void ShowRaceSelectionPanel()
        {
            _mainMenuPanel.SetActive(false);
            _raceSelectionPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _helpPanel.SetActive(false);
        }

        // Метод для перехода к панели настроек
        public void ShowSettingsPanel()
        {
            _mainMenuPanel.SetActive(false);
            _raceSelectionPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            _helpPanel.SetActive(false);
        }

        // Метод для перехода к панели помощи
        public void ShowHelpPanel()
        {
            _mainMenuPanel.SetActive(false);
            _raceSelectionPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _helpPanel.SetActive(true);
        }

        // Метод для возврата в главное меню
        public void ShowMainMenuPanel()
        {
            _mainMenuPanel.SetActive(true);
            _raceSelectionPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _helpPanel.SetActive(false);
        }

        // Метод для выхода из игры
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}