using UnityEngine;

namespace DefaultNamespace
{
    public class SkinBuyPlacement : MonoBehaviour
    {
        private GameObject _currentModel;
        private Vector3 _displayScale = new Vector3(90, 90, 90);
        
        public void InstantiateModel(GameObject model)
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
            
            _currentModel = Instantiate(model, transform);
            _currentModel.transform.localScale = _displayScale;
        }
    }
}