using UnityEngine;
using UnityEngine.UI;

namespace Hero
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private bool _isAttack;
        private Player _player;
        
        public void Initialized(Player player)
        {
            _player = player;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(AttackWeapon);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AttackWeapon);
        }

        private void AttackWeapon()
        {
            _player.UseAttack();
        }
    }
}