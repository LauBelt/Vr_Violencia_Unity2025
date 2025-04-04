using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    private Animator animator;

    // Referencia al punto de anclaje donde se debe fijar la silla (por ejemplo, la mano del personaje)
    public Transform attachPoint;
    public Transform grabPoint;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("ChairController: No se encontró Animator en la silla.");
        }
    }

    // Método para activar la animación de levantar la silla y asociarla al personaje
    public void LiftChair()
    {
        if (animator != null)
        {
            animator.SetTrigger("Lift");
        }

        // Una vez iniciada la animación, parentamos la silla al attachPoint
        if (attachPoint != null && grabPoint != null)
        {
            // Reparenta la silla, pero ajusta la posición según la diferencia entre el grabPoint y el pivote
            transform.SetParent(attachPoint);
            // La posición local se ajusta para que el grabPoint quede en la posición del attachPoint.
            // Por ejemplo, si grabPoint estaba 0.3 unidades arriba y 0.4 hacia atrás del pivote, usamos esos valores.
            Vector3 offset = grabPoint.localPosition;
            // Para que el grabPoint se alinee con el attachPoint, la silla se debe desplazar en la dirección opuesta a ese offset.
            transform.localPosition = -offset;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("ChairController: attachPoint no está asignado.");
        }
    }
}
