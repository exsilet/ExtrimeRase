using SO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        //[SerializeField] private TMP_Text _nameWeapon;
        [SerializeField] private Text _nameWeapon;
        [SerializeField] private Image _icon;
        [SerializeField] private Transform _view;

        private Weapon _weapon;

        private void Start() => 
            _view.gameObject.SetActive(false);

        public void Render(Weapon weapon)
        {
            _weapon = weapon;

            _nameWeapon.text = weapon.Label;
            _icon.sprite = weapon.Icon;
            _view.gameObject.SetActive(true);
        }

        public void UsedWeapon()
        {
            _weapon = null;
            _nameWeapon.text = null;
            _icon.sprite = null;
            _view.gameObject.SetActive(false);
        }
    }
}