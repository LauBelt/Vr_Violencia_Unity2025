using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionIgnorada : MonoBehaviour
{
    [TextArea(3, 10)]
    public string ignoreReflectionText; // Texto de reflexion para la decision de ignorar
    [TextArea(3, 10)]
    public string ignoreReflectionText_OtroColor;
    public GameObject reflectionPanel; // Panel UI para mostrar la reflexion
    public TextMeshProUGUI reflectionTextComponent; // Componente TextMesh Pro del texto de reflexion
    public MicroagresionTrigger microaggressionTriggerScript;
    public AudioClip ignoreButtonAudioClip;
    public Color colorNegro = Color.black;
    public Color colorOtro = Color.red;
    public float animationDelay = 5f; // Tiempo de la animación en segundos
    public AudioClip microaggressionEndAudio; // Audio de "finalizó microagresión"
    // Para el personaje que debe caminar
    public bool requiresWalkingAnimation = false;
    public CharacterControllerVR characterController;
    // Para el personaje que debe levantar la silla
    public bool requiresChairLiftAnimation = false;
    public Animator characterAnimator;
    public ChairController chairController;
    public string liftChairTriggerName = "LiftChair";
    public float reparentDelay = 0.5f;
    public StatisticsManager statisticsManager;

    public void OnIgnoreButtonClicked()
    {
        if (statisticsManager != null)
        {
            statisticsManager.IncrementIgnored();
        }
        // Mostrar panel de reflexion
        if (reflectionPanel != null && reflectionTextComponent != null)
        {
            reflectionTextComponent.text = $"<color=#{ColorUtility.ToHtmlStringRGB(colorNegro)}>{ignoreReflectionText}</color> <color=#{ColorUtility.ToHtmlStringRGB(colorOtro)}>{ignoreReflectionText_OtroColor}</color>";
            reflectionPanel.SetActive(true); // Muestra el panel de reflexion
            StartCoroutine(FinalizeMicroaggression());
        }
        // Reproducir audio 
        if (ignoreButtonAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(ignoreButtonAudioClip, transform.position);
        }
        if (requiresWalkingAnimation && characterController != null)
        {
            characterController.StartWalking();
        }
        if (requiresChairLiftAnimation && chairController != null)
        {
            StartCoroutine(CrouchThenLiftChair());
        }
    }

    IEnumerator CrouchThenLiftChair()
    {
        // Activar la animación de levantar la silla
        if (characterAnimator != null)
        {
            characterAnimator.ResetTrigger(liftChairTriggerName);
            characterAnimator.SetTrigger(liftChairTriggerName);
            Debug.Log("DecisionIgnorada: Trigger " + liftChairTriggerName + " activado en characterAnimator.");
        }
        else
        {
            Debug.LogWarning("DecisionIgnorada: characterAnimator no está asignado.");
        }

        // Espera para sincronizar el momento en que se re-parenta la silla (ajusta este delay según la animación)
        yield return new WaitForSeconds(reparentDelay);

        // Llamar al método del ChairController para re-parentar la silla a la mano del personaje
        if (chairController != null)
        {
            chairController.LiftChair();
            Debug.Log("DecisionIgnorada: chairController.LiftChair() llamado.");
        }
        else
        {
            Debug.LogWarning("DecisionIgnorada: chairController no está asignado.");
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
        if (reflectionPanel != null)
        {
            reflectionPanel.SetActive(false);
            Debug.Log("DecisionIgnorada: reflectionPanel ocultado.");
        }
    }
}

