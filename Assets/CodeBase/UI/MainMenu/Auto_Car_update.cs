using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class AutoServiceController : MonoBehaviour
    {
        [System.Serializable]
        public class CarCard
        {
            public GameObject[] upgradeLevels; // Массив моделей машины для каждого уровня улучшений
        }

        public CarCard[] cars; // Массив карточек машин
        public Transform carDisplayPoint; // Точка для отображения машины
        public Button nextCarButton; // Кнопка для перехода к следующей машине
        public Button previousCarButton; // Кнопка для перехода к предыдущей машине
        public Button upgradeButton; // Кнопка для улучшения машины
        public Button backButton; // Кнопка для возврата на панель гаража

        private int currentCarIndex = 0; // Текущая машина
        private int[] carUpgradeLevels; // Массив уровней улучшений для каждой машины
        private GameObject currentCarInstance; // Текущая отображаемая машина

        private void Start()
        {
            // Инициализация массива уровней улучшений
            carUpgradeLevels = new int[cars.Length];
            for (int i = 0; i < cars.Length; i++)
            {
                carUpgradeLevels[i] = 0; // Изначально все машины на уровне 0
            }

            // Подключение кнопок к методам
            nextCarButton.onClick.AddListener(ShowNextCar);
            previousCarButton.onClick.AddListener(ShowPreviousCar);
            upgradeButton.onClick.AddListener(ApplyUpgrade);
            backButton.onClick.AddListener(ReturnToGarage);

            // Инициализация отображения машины
            UpdateCarDisplay();
        }

        private void UpdateCarDisplay()
        {
            // Удаляем предыдущую машину
            if (currentCarInstance != null)
            {
                Destroy(currentCarInstance);
            }

            // Создаем и отображаем текущую машину с соответствующим уровнем улучшения
            int upgradeLevel = carUpgradeLevels[currentCarIndex];
            currentCarInstance = Instantiate(cars[currentCarIndex].upgradeLevels[upgradeLevel], carDisplayPoint.position, carDisplayPoint.rotation);
            currentCarInstance.transform.SetParent(carDisplayPoint); // Устанавливаем объект как дочерний для правильного позиционирования
        }

        private void ShowNextCar()
        {
            // Переключение на следующую машину
            currentCarIndex = (currentCarIndex + 1) % cars.Length;
            UpdateCarDisplay();
        }

        private void ShowPreviousCar()
        {
            // Переключение на предыдущую машину
            currentCarIndex = (currentCarIndex - 1 + cars.Length) % cars.Length;
            UpdateCarDisplay();
        }

        private void ApplyUpgrade()
        {
            // Увеличение уровня улучшения, если ещё не достигнут максимум
            int currentUpgradeLevel = carUpgradeLevels[currentCarIndex];
            if (currentUpgradeLevel < cars[currentCarIndex].upgradeLevels.Length - 1)
            {
                carUpgradeLevels[currentCarIndex]++;
                UpdateCarDisplay();

                // Запуск анимации подпрыгивания
                StartCoroutine(BounceAnimation());
            }
        }

        private void ReturnToGarage()
        {
            // Логика возврата на панель гаража
            Debug.Log("Возврат на панель гаража");
            // Реализуйте логику возврата, например, скрытие этой панели и показ панели гаража.
            // Или если используется другая сцена, можно загрузить сцену гаража.
            // Пример: SceneManager.LoadScene("GarageScene");
        }

        private IEnumerator BounceAnimation()
        {
            Vector3 originalPosition = currentCarInstance.transform.localPosition;
            float bounceHeight = 2f; // высота подпрыгивания
            float bounceTime = 0.2f; // время на подпрыгивание

            // Подпрыгивание вверх
            float elapsedTime = 0;
            while (elapsedTime < bounceTime)
            {
                currentCarInstance.transform.localPosition = Vector3.Lerp(originalPosition, originalPosition + Vector3.forward * bounceHeight, (elapsedTime / bounceTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Возвращение вниз
            elapsedTime = 0;
            while (elapsedTime < bounceTime)
            {
                currentCarInstance.transform.localPosition = Vector3.Lerp(originalPosition + Vector3.forward * bounceHeight, originalPosition, (elapsedTime / bounceTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            currentCarInstance.transform.localPosition = originalPosition;
        }
    }
}