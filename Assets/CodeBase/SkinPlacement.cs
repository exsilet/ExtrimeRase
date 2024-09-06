using UnityEngine;

namespace DefaultNamespace
{
    public class SkinPlacement : MonoBehaviour
    {
        [SerializeField] private GameObject[] _carsObjects;
        
        private GameObject _currentModel;

        public void InstantiateModel(GameObject model, int index)
        {
            if (_currentModel != null)
                return;
            
            foreach (GameObject car in _carsObjects)
            {
                car.SetActive(false);
            }
            
            _carsObjects[index].SetActive(true);
            
            //_currentModel = Instantiate(model, transform);
        }
    }
}