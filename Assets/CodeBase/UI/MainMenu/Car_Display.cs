using UnityEngine;

namespace UI.MainMenu
{
    public class CarDisplayController : MonoBehaviour
    {
        public GameObject[] cars; // Массив машин для отображения
        private int currentCarIndex = 0;

        public Vector3 displayPosition = new Vector3(0, 0, 0); // Позиция для отображения машин
        public Vector3 displayRotation = new Vector3(0, 0, 0); // Вращение для отображения машин
        public Vector3 displayScale = new Vector3(1, 1, 1); // Масштаб для отображения машин

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

        void DisplayCar(int index)
        {
            foreach (GameObject car in cars)
            {
                car.SetActive(false); // Скрыть все машины
            }

            cars[index].SetActive(true); // Показать текущую машину
            //cars[index].transform.position = displayPosition; // Установить позицию машины
            //cars[index].transform.rotation = Quaternion.Euler(displayRotation); // Установить поворот машины
            cars[index].transform.localScale = displayScale; // Установить масштаб машины
        }
    }
}