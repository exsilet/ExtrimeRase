using UnityEngine;

public class CarRespawn : MonoBehaviour
{
    [SerializeField] private CarSmokeEffect _carSmokeEffect; // Ссылка на скрипт дыма
    [SerializeField] private Hero.HeroHealth _heroHealth;    // Ссылка на скрипт здоровья

    public void RespawnCar()
    {
        // Сбрасываем здоровье машины
        _heroHealth.ResetHealth();

        // Сбрасываем дым
        _carSmokeEffect.ResetSmoke();

        // Активируем машину (или другие действия по респауну)
        gameObject.SetActive(true);
    }
}