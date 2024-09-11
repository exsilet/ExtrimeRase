using UnityEngine;

public class CarCollisionSound : MonoBehaviour
{
    public AudioSource audioSource;  // Ссылка на AudioSource
    public string curbTag = "Curb";  // Тэг бордюра

    void Start()
    {
        // Получаем компонент AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулись ли с бордюром по тэгу и если звук не проигрывается
        if (collision.gameObject.CompareTag(curbTag) && !audioSource.isPlaying)
        {
            // Воспроизводим звук
            audioSource.Play();
        }
    }
}