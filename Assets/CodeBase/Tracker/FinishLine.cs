using UnityEngine;

namespace Tracker
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out LapCounter lapCounter))
            {
                if (lapCounter != null)
                {
                    lapCounter.IncrementLap();
                }
            }
        }
    }
}