using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsGraph : MonoBehaviour
{
    // Referencias a las imágenes que actuarán como barras
    public Image ignoredBar;
    public Image confrontedBar;

    // Textos que mostrarán los números
    public TextMeshProUGUI ignoredCountText;
    public TextMeshProUGUI confrontedCountText;

    public void UpdateGraph(int countIgnored, int countConfronted)
    {
        // Se toma el máximo entre ambos contadores para normalizar los valores
        int maxCount = Mathf.Max(countIgnored, countConfronted, 1);
        
        // Calcula el porcentaje para cada barra (valor entre 0 y 1)
        float ignoredFill = (float)countIgnored / maxCount;
        float confrontedFill = (float)countConfronted / maxCount;
        
        // Actualiza el fill amount de las imágenes
        ignoredBar.fillAmount = ignoredFill;
        confrontedBar.fillAmount = confrontedFill;
        
        // Actualiza los textos numéricos
        ignoredCountText.text = "Ignorados: " + countIgnored;
        confrontedCountText.text = "Confrontados: " + countConfronted;
    }
}
