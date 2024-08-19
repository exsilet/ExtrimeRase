using System.Collections.Generic;
using SO;
using UnityEngine;

namespace UI
{
    public class PanelVictory : MonoBehaviour
    {
        [SerializeField] private Transform _panelVictory;
        [SerializeField] private List<HeroesData> _heroes;
        [SerializeField] private TopPlayers _playersPrefab;
        [SerializeField] private Transform _parent;

        private readonly int _topThreePlayers = 3;

        private int _money;
        private int _point;

        public void OpenPanel()
        {
            for (int i = 0; i < _heroes.Count; i++)
            {
                var players = Instantiate(_playersPrefab, _parent);
                
                if (_topThreePlayers < i)
                {
                    players.Initialized(_heroes[i], i, _money, _point);
                }
            }
        }
    }
}