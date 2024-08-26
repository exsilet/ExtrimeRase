using UnityEngine;

namespace UI.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        public GameObject mainMenuPanel; // Панель главного меню
        public GameObject shopPanel; // Панель магазина
        public GameObject autoServicePanel; // Панель авто-сервиса

        // Метод для перехода в магазин
        public void GoToShop()
        {
            mainMenuPanel.SetActive(false); // Скрываем главное меню
            shopPanel.SetActive(true); // Показываем магазин
            autoServicePanel.SetActive(false); // Скрываем авто-сервис
        }

        // Метод для перехода в авто-сервис
        public void GoToAutoService()
        {
            mainMenuPanel.SetActive(false); // Скрываем главное меню
            shopPanel.SetActive(false); // Скрываем магазин
            autoServicePanel.SetActive(true); // Показываем авто-сервис
        }

        // Метод для возврата в главное меню
        public void BackToMainMenu()
        {
            mainMenuPanel.SetActive(true); // Показываем главное меню
            shopPanel.SetActive(false); // Скрываем магазин
            autoServicePanel.SetActive(false); // Скрываем авто-сервис
        }
    }
}

