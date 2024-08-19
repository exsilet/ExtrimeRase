using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "CarData", menuName = "Shop/CarData")]
    public class CarItem : CarStaticData
    {
        [field: SerializeField] public CarType CarType { get; private set; }
    }
}