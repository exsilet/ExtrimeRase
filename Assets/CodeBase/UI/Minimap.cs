using UnityEngine;

namespace UI
{
    public class Minimap : MonoBehaviour
    {
        private Transform _player;
        
        public void Initialized(Transform player)
        {
            _player = player;
        }

        private void LateUpdate()
        {
            transform.eulerAngles = new Vector3(0f, _player.eulerAngles.y, 0f);
        }
    }
}