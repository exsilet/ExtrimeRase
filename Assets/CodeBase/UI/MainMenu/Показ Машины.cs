using UnityEngine;

namespace UI.MainMenu
{
    public class CarRotation : MonoBehaviour
    {
        public float rotationSpeed = 30f;  // Скорость вращения в градусах в секунду

        void Update()
        {
            // Вращение вокруг оси Y
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}