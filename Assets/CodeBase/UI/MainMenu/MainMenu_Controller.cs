using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenu_Controller : MonoBehaviour
    {
        // Панели меню
        public GameObject mainMenuPanel;
        public GameObject raceSelectionPanel;
        public GameObject settingsPanel;
        public GameObject helpPanel;

        // Метод для перехода к панели гонок
        public void ShowRaceSelectionPanel()
        {
            mainMenuPanel.SetActive(false);
            raceSelectionPanel.SetActive(true);
            settingsPanel.SetActive(false);
            helpPanel.SetActive(false);
        }

        // Метод для перехода к панели настроек
        public void ShowSettingsPanel()
        {
            mainMenuPanel.SetActive(false);
            raceSelectionPanel.SetActive(false);
            settingsPanel.SetActive(true);
            helpPanel.SetActive(false);
        }

        // Метод для перехода к панели помощи
        public void ShowHelpPanel()
        {
            mainMenuPanel.SetActive(false);
            raceSelectionPanel.SetActive(false);
            settingsPanel.SetActive(false);
            helpPanel.SetActive(true);
        }

        // Метод для возврата в главное меню
        public void ShowMainMenuPanel()
        {
            mainMenuPanel.SetActive(true);
            raceSelectionPanel.SetActive(false);
            settingsPanel.SetActive(false);
            helpPanel.SetActive(false);
        }

        // Метод для выхода из игры
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}