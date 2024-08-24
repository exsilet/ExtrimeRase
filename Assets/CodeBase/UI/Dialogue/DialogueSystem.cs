using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private float _typingSpeed = 0.02f;
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private string[] _line;
        [SerializeField] private Transform _dialogPlayer;
        [SerializeField] private Transform _dialogEnemy;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Transform _dialogue;
        [SerializeField] private StartBattle _startBattle;
        
        private int _index;
        private bool _swapDialog;
        private bool _dialogueStart;

        public bool DialogueStart => _dialogueStart;

        private void Start()
        {
            _dialogueStart = false;
            _buttonNext.gameObject.SetActive(false);
            _dialogEnemy.SetSiblingIndex(1);
            _swapDialog = true;
            _dialogueText.text = String.Empty;
            StartDialogue();
        }

        public void NextText()
        {
            if (_dialogueText.text == _line[_index])
            {
                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                _dialogueText.text = _line[_index];
            }
        }

        private void StartDialogue()
        {
            _index = 0;
            StartCoroutine(TypeSentence());
        }

        private void DisplayNextSentence()
        {
            if (_index < _line.Length-1)
            {
                _swapDialog = !_swapDialog;
                _index++;
                _dialogueText.text = String.Empty;
                
                StartCoroutine(TypeSentence());
                
                if (_swapDialog)
                {
                    _dialogPlayer.SetSiblingIndex(0);
                    _dialogEnemy.SetSiblingIndex(1);
                }
                else
                {
                    _dialogPlayer.SetSiblingIndex(1);
                    _dialogEnemy.SetSiblingIndex(0);
                }
                
                
            }
            else
            {
                _dialogueStart = true;
                _dialogue.gameObject.SetActive(false);
                _startBattle.enabled = true;
            }
        }

        private IEnumerator TypeSentence()
        {
            foreach (char letter in _line[_index].ToCharArray())
            {
                _dialogueText.text += letter;
                _buttonNext.gameObject.SetActive(false);
                yield return new WaitForSeconds(_typingSpeed);
            }
            
            _buttonNext.gameObject.SetActive(true);
        }
    }
}