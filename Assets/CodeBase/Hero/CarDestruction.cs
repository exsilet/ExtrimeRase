using System.Collections;
using UnityEngine;

public class CarDestruction : MonoBehaviour
{
    [SerializeField] private GameObject _wreckPrefab;       // Префаб для разрушенной машины (например, дым или металлолом)
    [SerializeField] private Hero.HeroHealth _heroHealth;   // Ссылка на скрипт здоровья
    [SerializeField] private AudioSource _destructionSound; // Звук разрушения
    [SerializeField] private float _wreckDelay = 2f;        // Задержка перед появлением префаба дыма
    [SerializeField] private GameObject _smokePrefab;       // Префаб дыма

    private bool _isDestroyed = false;

    private void Start()
    {
        // Подписываемся на событие смерти, если есть
        if (_heroHealth != null)
        {
            _heroHealth.Died += OnCarDestroyed;
        }

        if (_smokePrefab != null)
        {
            _smokePrefab.SetActive(false); // Дым выключен по умолчанию
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события смерти, когда объект уничтожается
        if (_heroHealth != null)
        {
            _heroHealth.Died -= OnCarDestroyed;
        }
    }

    private void OnCarDestroyed(Hero.HeroHealth health)
    {
        if (_isDestroyed) return; // Если уже уничтожена, ничего не делаем
        _isDestroyed = true;

        // Воспроизводим звук разрушения
        if (_destructionSound != null)
        {
            _destructionSound.Play();
        }

        // Активируем дым немедленно
        if (_smokePrefab != null)
        {
            _smokePrefab.SetActive(true); // Активируем префаб дыма
        }

        // Запускаем появление металлолома с задержкой
        StartCoroutine(SpawnWreckAfterSound(_wreckDelay));
    }

    private IEnumerator SpawnWreckAfterSound(float delay)
    {
        // Ждем завершения звука или задержки
        yield return new WaitForSeconds(delay);

        // Создаем объект металлолома (или другой префаб) на месте машины
        if (_wreckPrefab != null)
        {
            Instantiate(_wreckPrefab, transform.position, transform.rotation);
        }

        // Отключаем машину после того, как появился префаб дыма или металлолома
        gameObject.SetActive(false);
    }
}