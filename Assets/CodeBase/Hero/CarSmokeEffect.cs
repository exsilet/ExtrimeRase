using UnityEngine;

public class CarSmokeEffect : MonoBehaviour
{
    [SerializeField] private GameObject _smokePrefab; // Префаб дыма
    [SerializeField] private Hero.HeroHealth _heroHealth; // Ссылка на скрипт здоровья
    private bool _isSmoking = false;

    private void Start()
    {
        if (_smokePrefab != null)
        {
            _smokePrefab.SetActive(false); // Дым выключен по умолчанию
        }
    }

    private void Update()
    {
        // Проверяем, если здоровья меньше половины и дым еще не активирован
        if (_heroHealth.Current <= _heroHealth.Max / 2 && !_isSmoking)
        {
            StartSmoking();
        }
        else if (_heroHealth.Current > _heroHealth.Max / 2 && _isSmoking)
        {
            StopSmoking(); // Останавливаем дым, если здоровье восстановилось
        }
    }

    private void StartSmoking()
    {
        if (_smokePrefab != null)
        {
            _smokePrefab.SetActive(true); // Активируем префаб дыма
            _isSmoking = true; // Устанавливаем флаг, чтобы дым не запускался несколько раз
        }
    }

    private void StopSmoking()
    {
        if (_smokePrefab != null)
        {
            _smokePrefab.SetActive(false); // Останавливаем дым
            _isSmoking = false; // Сбрасываем флаг
        }
    }

    public void ResetSmoke()
    {
        StopSmoking(); // Сбрасываем состояние дыма
    }
}