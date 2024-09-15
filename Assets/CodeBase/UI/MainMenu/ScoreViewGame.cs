using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ScoreViewGame : MonoBehaviour
    {
        [SerializeField] private Text _scorePosition1Text;
        [SerializeField] private Text _scorePosition2Text;
        [SerializeField] private Text _scorePosition3Text;

        private readonly int _pointsFor1stPlace = 400;
        private readonly int _pointsFor2ndPlace = 200;
        private readonly int _pointsFor3rdPlace = 100;

        public int PointsFor1stPlace => _pointsFor1stPlace;
        public int PointsFor2ndPlace => _pointsFor2ndPlace;
        public int PointsFor3rdPlace => _pointsFor3rdPlace;

        public void Initialize()
        {
            _scorePosition1Text.text = _pointsFor1stPlace.ToString();
            _scorePosition2Text.text = _pointsFor2ndPlace.ToString();
            _scorePosition3Text.text = _pointsFor3rdPlace.ToString();
        }

        public int GetScorePlace(int place)
        {
            return place switch
            {
                1 => _pointsFor1stPlace,
                2 => _pointsFor2ndPlace,
                3 => _pointsFor3rdPlace,
                _ => 0
            };
        }
    }
}