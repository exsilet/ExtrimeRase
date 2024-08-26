using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class CarMenuController : MonoBehaviour
    {
        public GameObject[] cars; // Массив 3D машин
        private int currentCarIndex = 0;

        public Transform carDisplayPoint; // Точка отображения машины

        void Start()
        {
            DisplayCar(currentCarIndex); // Показать первую машину при старте
        }

        public void ShowNextCar()
        {
            currentCarIndex++;
            if (currentCarIndex >= cars.Length)
            {
                currentCarIndex = 0; // Перейти к первой машине, если достигли конца массива
            }
            DisplayCar(currentCarIndex);
        }

        public void ShowPreviousCar()
        {
            currentCarIndex--;
            if (currentCarIndex < 0)
            {
                currentCarIndex = cars.Length - 1; // Перейти к последней машине, если достигли начала массива
            }
            DisplayCar(currentCarIndex);
        }

        public void BuyCar()
        {
            // Логика покупки машины (может быть добавлена здесь)
            Debug.Log("Машина куплена: " + cars[currentCarIndex].name);
        }

        public void BackToRaceMenu()
        {
            SceneManager.LoadScene("RaceMenu"); // Загрузка сцены меню гонки
        }

        void DisplayCar(int index)
        {
            foreach (GameObject car in cars)
            {
                car.SetActive(false); // Скрыть все машины
            }

            cars[index].SetActive(true); // Показать текущую машину
            cars[index].transform.position = carDisplayPoint.position; // Установить позицию машины
            cars[index].transform.rotation = carDisplayPoint.rotation; // Установить поворот машины
        }
    }
}