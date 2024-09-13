using ArcadeVP;
using UnityEngine;

namespace Weapons
{
    public class Oil : MonoBehaviour
    {
        [SerializeField] private float _timeDelay;
        //[SerializeField] private float slipFactor = 2.0f;

        private float _originalDrag;
        private float _originalAngularDrag;

        private void OnTriggerEnter(Collider other)
        {
            // Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            //
            // if (rigidbody != null)
            // {
            //     _originalDrag = rigidbody.drag;
            //     _originalAngularDrag = rigidbody.angularDrag;
            //     
            //     rigidbody.drag /= slipFactor;
            //     rigidbody.angularDrag /= slipFactor;
            // }
            
            if (other.gameObject.TryGetComponent(out ArcadeAiVehicleController enemy))
            {
                enemy.OilZone();
                Destroy(gameObject, _timeDelay);
            }
            
            if (other.gameObject.TryGetComponent(out ArcadeVehicleController player))
            {
                player.OilZone();
                Destroy(gameObject, _timeDelay);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ArcadeAiVehicleController enemy))
            {
                enemy.OilZone();
            }
            
            if (other.gameObject.TryGetComponent(out ArcadeVehicleController player))
            {
                player.OilZone();
            }
        }
    }
}