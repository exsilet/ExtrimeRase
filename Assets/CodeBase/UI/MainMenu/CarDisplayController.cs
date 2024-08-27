using UnityEngine;

namespace UI.MainMenu
{
    public class CarDisplayController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _carsObjects; // Массив машин для отображения

        public Vector3 displayPosition = new Vector3(0, 0, 0); // Позиция для отображения машин
        public Vector3 displayRotation = new Vector3(0, 0, 0); // Вращение для отображения машин
        public Vector3 displayScale = new Vector3(1, 1, 1); // Масштаб для отображения машин

        private int _currentCarIndex = 0;
        
        private void Start()
        {
            DisplayCar(_currentCarIndex);
        }

        public void ShowNextCar()
        {
            _currentCarIndex++;
            if (_currentCarIndex >= _carsObjects.Length)
            {
                _currentCarIndex = 0;
            }
            DisplayCar(_currentCarIndex);
        }

        public void ShowPreviousCar()
        {
            _currentCarIndex--;
            if (_currentCarIndex < 0)
            {
                _currentCarIndex = _carsObjects.Length - 1;
            }
            DisplayCar(_currentCarIndex);
        }

        private void DisplayCar(int index)
        {
            foreach (GameObject car in _carsObjects)
            {
                car.SetActive(false);
            }

            _carsObjects[index].SetActive(true); // Показать текущую машину
            //cars[index].transform.position = displayPosition; // Установить позицию машины
            //cars[index].transform.rotation = Quaternion.Euler(displayRotation); // Установить поворот машины
            _carsObjects[index].transform.localScale = displayScale; // Установить масштаб машины
        }
    }
}