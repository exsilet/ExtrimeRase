using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TopPlayers : MonoBehaviour
    {
        [SerializeField] private Image _iconPlayer;
        [SerializeField] private Image _iconGoldTrophy;
        [SerializeField] private Image _iconSilverTrophy;
        [SerializeField] private Image _iconBronzeTrophy;
        [SerializeField] private TMP_Text _namePlayer;
        [SerializeField] private TMP_Text _currentMoney;
        [SerializeField] private TMP_Text _currentPoint;

        public void Initialized(HeroesData player, int index, int money, int point)
        {
            _iconPlayer.sprite = player.IconPlayer;
            _namePlayer.text = player.Name;

            switch (index)
            {
                case 0:
                    TopTrophy(true, false, false);
                    break;
                case 1:
                    TopTrophy(false, true, false);
                    break;
                case 2:
                    TopTrophy(false, false, true);
                    break;
            }

            _currentPoint.text = point.ToString();
            _currentMoney.text = money.ToString();
        }

        private void TopTrophy(bool gold, bool silver, bool bronze)
        {
            _iconGoldTrophy.gameObject.SetActive(gold);
            _iconSilverTrophy.gameObject.SetActive(silver);
            _iconBronzeTrophy.gameObject.SetActive(bronze);
        }
    }
}