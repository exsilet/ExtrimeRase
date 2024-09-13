using UnityEngine;
using UnityEngine.UI;

namespace UI.VictoryPanel
{
    public class TopPlayerGame : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Initialized(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}