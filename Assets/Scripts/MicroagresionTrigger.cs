using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class MicroagresionTrigger : MonoBehaviour
{
    public AudioClip microaggressionAudio;
    [TextArea(3, 10)]
    public string microaggressionSubtitle;
    public GameObject subtitlesPanel;
    public TextMeshProUGUI subtitlesText;
    private bool microaggressionActive = false;
    public float delayBeforeActivation = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !microaggressionActive)
        {
            Debug.Log("MicroaggressionTrigger: Jugador llegó al tapete. Iniciando retraso de activación...");
            StartCoroutine(DelayedMicroaggressionActivation());
        }
    }
    IEnumerator DelayedMicroaggressionActivation()
    {
        Debug.Log("MicroaggressionTrigger: Corutina de retraso iniciada. Esperando " + delayBeforeActivation + " segundos...");
        yield return new WaitForSeconds(delayBeforeActivation);

        Debug.Log("MicroaggressionTrigger: Tiempo de retraso completado. Activando microagresión.");
        TriggerMicroaggressionInternal();
    }

    private void TriggerMicroaggressionInternal()
    {
        microaggressionActive = true;

        // Activar audio
        if (microaggressionAudio != null)
        {
            AudioSource.PlayClipAtPoint(microaggressionAudio, transform.position);
        }

        // Mostrar subtítulos
        if (subtitlesPanel != null && subtitlesText != null)
        {
            subtitlesText.text = microaggressionSubtitle;
            subtitlesPanel.SetActive(true);
        }
    }
    public void TriggerMicroaggression()
    {
        Debug.Log("MicroaggressionTrigger: Activación de microagresión INMEDIATA (sin retraso) solicitada.");
        TriggerMicroaggressionInternal();
    }

    public void HideSubtitlesPanel()
    {
        Debug.Log("MicroagresionTrigger: HideSubtitlesPanel() llamado.");
        if (subtitlesPanel != null)
            subtitlesPanel.SetActive(false);
        microaggressionActive = false;
    }
}
