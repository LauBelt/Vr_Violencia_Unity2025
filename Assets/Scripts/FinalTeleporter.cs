using UnityEngine;

public class FinalTeleporter : MonoBehaviour
{
    public StatisticsManager statisticsManager; // Asigna este componente en el Inspector
    public GameObject PanelFin;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (statisticsManager != null)
            {
                statisticsManager.ShowFinalStatsPanel();
            }
        }
    }
}
