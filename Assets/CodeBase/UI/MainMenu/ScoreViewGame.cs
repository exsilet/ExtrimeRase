using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ScoreViewGame : MonoBehaviour
    {
        [SerializeField] private Text _scorePosition1Text;
        [SerializeField] private Text _scorePosition2Text;
        [SerializeField] private Text _scorePosition3Text;
        [SerializeField] private Text _moneyPosition1Text;
        [SerializeField] private Text _moneyPosition2Text;
        [SerializeField] private Text _moneyPosition3Text;
        
        [SerializeField] private int _pointsFor1stPlace = 400;
        [SerializeField] private int _pointsFor2ndPlace = 200;
        [SerializeField] private int _pointsFor3rdPlace = 100;
        [SerializeField] private int _moneyFor1stPlace = 20000;
        [SerializeField] private int _moneyFor2ndPlace = 12500;
        [SerializeField] private int _moneyFor3rdPlace = 5000;

        public int PointsFor1stPlace => _pointsFor1stPlace;
        public int PointsFor2ndPlace => _pointsFor2ndPlace;
        public int PointsFor3rdPlace => _pointsFor3rdPlace;

        public void Initialize()
        {
            _scorePosition1Text.text = _pointsFor1stPlace.ToString();
            _scorePosition2Text.text = _pointsFor2ndPlace.ToString();
            _scorePosition3Text.text = _pointsFor3rdPlace.ToString();

            _moneyPosition1Text.text = _moneyFor1stPlace.ToString();
            _moneyPosition2Text.text = _moneyFor2ndPlace.ToString();
            _moneyPosition3Text.text = _moneyFor3rdPlace.ToString();
        }

        public int GetScorePlace(int place)
        {
            switch (place)
            {
                case 1:
                    return _pointsFor1stPlace;
                case 2:
                    return _pointsFor2ndPlace;
                case 3:
                    return _pointsFor3rdPlace;
                default:
                    return 0;
            }
        }
        
        public int GetMoneyPlace(int place)
        {
            switch (place)
            {
                case 1:
                    return _moneyFor1stPlace;
                case 2:
                    return _moneyFor2ndPlace;
                case 3:
                    return _moneyFor3rdPlace;
                default:
                    return 0;
            }
        }
    }
}