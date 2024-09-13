using ArcadeVP;
using Enemies;
using Hero;
using UI.VictoryPanel;
using UnityEngine;

namespace Tracker
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private PanelVictory _panelVictory;

        private void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.TryGetComponent(out LapCounter lapCounter))
            // {
            //     if (lapCounter != null)
            //     {
            //         lapCounter.IncrementLap();
            //     }
            // }

            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                var enemyLap = enemy.GetComponent<LapCounter>();

                if (enemyLap.CurrentLap == _panelVictory.FinishingLap)
                {
                    _panelVictory.Initialized(other.GetComponent<Enemy>().HeroesEnemyData);
                    enemy.GetComponent<ArcadeAiVehicleController>().IsPaused(true);
                }
            }

            if (other.gameObject.TryGetComponent(out Player player))
            {
                var playerLap = player.GetComponent<LapCounter>();

                if (playerLap.CurrentLap == _panelVictory.FinishingLap)
                {
                    _panelVictory.Initialized(other.GetComponent<Player>().Heroes);
                    player.GetComponent<ArcadeVehicleController>().IsPaused(true);
                    _panelVictory.OpenPanel();
                }
            }
        }
    }
}