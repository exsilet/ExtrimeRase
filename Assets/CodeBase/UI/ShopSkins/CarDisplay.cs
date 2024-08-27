using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class CarDisplay : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _carsObjects;
        [SerializeField] private Image _shadowImage;

        private List<CharacterSkins> _characterSkins = new();
        private int _currentCarIndex = 0;
        public Vector3 displayScale = new Vector3(1, 1, 1);

        public void Initialize(CharacterSkins characterSkins)
        {
            _characterSkins.Add(characterSkins);
        }
        
        public void ShowNextCar()
        {
            _currentCarIndex++;
            if (_currentCarIndex >= _characterSkins.Count)
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
                _currentCarIndex = _characterSkins.Count - 1;
            }
            DisplayCar(_currentCarIndex);
        }

        private void DisplayCar(int index)
        {
            foreach (GameObject car in _carsObjects)
            {
                car.SetActive(false);
            }

            _carsObjects[index].SetActive(true);
            _shadowImage.gameObject.SetActive(true);
            _carsObjects[index].transform.localScale = displayScale;
        }
    }
}