using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionConfrontada : MonoBehaviour
{
    [TextArea(3, 10)]
    public string confrontReflectionText;
    [TextArea(3, 10)]
    public string confrontReflectionText_OtroColor;
    public GameObject reflectionPanel;
    public TextMeshProUGUI reflectionTextComponent;
    public MicroagresionTrigger microaggressionTriggerScript; // Referencia al script MicroaggressionTrigger de la zona
    public AudioClip confrontButtonAudioClip;
    public Color colorNegro = Color.black;
    public Color colorOtro = Color.green;
    public float animationDelay = 15f;
    public AudioClip microaggressionEndAudio;
    public StatisticsManager statisticsManager;


    public void OnConfrontButtonClicked()
    {
        if (statisticsManager != null)
        {
            statisticsManager.IncrementConfronted();
        }
        // Mostrar panel de reflexion
        if (reflectionPanel != null && reflectionTextComponent != null)
        {
            reflectionTextComponent.text = $"<color=#{ColorUtility.ToHtmlStringRGB(colorNegro)}>{confrontReflectionText}</color> <color=#{ColorUtility.ToHtmlStringRGB(colorOtro)}>{confrontReflectionText_OtroColor}</color>";
            reflectionPanel.SetActive(true); // Muestra el panel de reflexion
            StartCoroutine(FinalizeMicroaggression());
        }
        // Reproducir audio 
        if (confrontButtonAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(confrontButtonAudioClip, transform.position);
        }
    }

    IEnumerator FinalizeMicroaggression()
    {
        yield return new WaitForSeconds(animationDelay);

        // Ocultar paneles
        HideReflectionPanel();
        if (microaggressionTriggerScript != null)
        {
            microaggressionTriggerScript.HideSubtitlesPanel();
        }

        // Reproducir audio de finalización
        if (microaggressionEndAudio != null)
        {
            AudioSource.PlayClipAtPoint(microaggressionEndAudio, transform.position);
        }
    }

    public void HideReflectionPanel()
    {
        if (reflectionPanel != null) reflectionPanel.SetActive(false);
    }
}
