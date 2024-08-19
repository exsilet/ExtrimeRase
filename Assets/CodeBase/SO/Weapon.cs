using UnityEngine;
using Weapons;

namespace SO
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _secondTime;
        [SerializeField] private GameObject _weaponObject;
        
        public Sprite Icon => _icon;
        public string Label => _label;
        public WeaponType WeaponType => _weaponType;
        public float SecondTime => _secondTime;
        public GameObject WeaponObject => _weaponObject;

        public void Shoot(Transform shootPoint)
        {
            if (_bullet != null)
            {
                Instantiate(_bullet, shootPoint.position, shootPoint.rotation);
            }
        }
        public void Shoot(Transform shootPoint, Transform target)
        {
            if (_bullet != null)
            {
                var rocket = Instantiate(_bullet, shootPoint.position, shootPoint.rotation);
                rocket.GetComponent<Rocket>().Initialized(target);
            }
        }
    }
}