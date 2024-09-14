using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Используй для UI Text
//using TMPro; // Если используешь TextMeshPro

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private Text uiText; // Используй TextMeshProUGUI если TextMeshPro
    [SerializeField] private string fullText = "Это пример печатающегося текста...";
    [SerializeField] private float typingSpeed = 0.05f; // Скорость печати

    private string currentText = "";

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}