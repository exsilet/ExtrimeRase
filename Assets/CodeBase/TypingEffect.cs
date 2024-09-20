using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TypingEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text uiText; // Используй TextMeshProUGUI если TextMeshPro
    [SerializeField] private string fullText = "Это пример печатающегося текста...";
    [SerializeField] private float typingSpeed = 0.05f; // Скорость печати
    [SerializeField] private AudioSource typingSound; // Добавляем AudioSource для звука

    private string currentText = "";
    private Coroutine typingCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Останавливаем предыдущую корутину, если она запущена
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        // Начинаем печатать текст заново
        typingCoroutine = StartCoroutine(TypeText());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Останавливаем печать текста и звук при уходе с панели
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        if (typingSound.isPlaying)
        {
            typingSound.Stop(); // Останавливаем звук
        }

        // Можно очистить текст, если нужно
        // uiText.text = "";
    }

    IEnumerator TypeText()
    {
        currentText = "";
        typingSound.Play(); // Включаем звук печати при начале печати
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingSound.Stop(); // Останавливаем звук печати, когда текст напечатан
    }
}
