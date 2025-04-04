using UnityEngine;
using TMPro;

public class StatisticsManager : MonoBehaviour
{
    public int countIgnored = 0;
    public int countConfronted = 0;

    // Panel final
    public GameObject finalStatsPanel;
    public TextMeshProUGUI finalTitleText;
    public TextMeshProUGUI finalReflectionText;

    // Mensajes de reflexión
    [TextArea(2, 5)]
    public string confrontReflectionMessage = "Tu valentía al alzar la voz es un paso para construir espacios más respetuosos.Recordemos que en cualquier lugar, el cambio empieza cuando no guardamos silencio";
    [TextArea(2, 5)]
    public string ignoreReflectionMessage = "A veces, mirar hacia otro lado es más fácil, pero cada vez que ignoramos una microagresión, dejamos que siga ocurriendo. En nuestra ciudad, cada gesto cuenta para que todos se sientan valorados";
    [TextArea(2, 5)]
    public string balancedReflectionMessage = "Tus decisiones mostraron un equilibrio entre actuar y observar. Recuerda que todos podemos ser parte de un cambio positivo, incluso con las acciones más pequeñas.";

    // Referencia al gráfico
    public StatisticsGraph statisticsGraph;

    public void IncrementIgnored()
    {
        countIgnored++;
        Debug.Log("IncrementIgnored: " + countIgnored);
    }

    public void IncrementConfronted()
    {
        countConfronted++;
        Debug.Log("IncrementConfronted: " + countConfronted);
    }

    public void ShowFinalStatsPanel()
    {
        // Decide el título y la reflexión basados en las estadísticas
        if (countConfronted > countIgnored)
        {
            finalTitleText.text = "¡Muy bien!";
            finalReflectionText.text = confrontReflectionMessage;
        }
        else if (countIgnored > countConfronted)
        {
            finalTitleText.text = "¡Ojo!";
            finalReflectionText.text = ignoreReflectionMessage;
        }
        else
        {
            finalTitleText.text = "Una decisión equilibrada";
            finalReflectionText.text = balancedReflectionMessage;
        }

        // Actualiza el gráfico con los contadores actuales
        if (statisticsGraph != null)
        {
            statisticsGraph.UpdateGraph(countIgnored, countConfronted);
        }

        // Activa el panel final
        finalStatsPanel.SetActive(true);
    }
}
