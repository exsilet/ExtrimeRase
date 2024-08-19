using UnityEngine;

namespace SpritesCamera
{
    public class SpriteBillboard : MonoBehaviour
    {
        [SerializeField] private bool _freezeXZAxis = true;

        private void LateUpdate()
        {
            if (_freezeXZAxis)
            {
                transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
            }
            else
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}