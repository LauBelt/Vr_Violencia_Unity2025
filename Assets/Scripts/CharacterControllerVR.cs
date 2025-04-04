using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControllerVR : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination; // Objeto vacío que marca el destino
    private Animator animator;

    // Umbral para considerar que el agente llegó al destino
    public float stoppingThreshold = 0.5f;

    void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Configura el stoppingDistance con el umbral deseado
        agent.stoppingDistance = stoppingThreshold;
    }

    void Update()
    {
        // Si el agente se está moviendo, aseguramos que la animación se reproduzca a velocidad normal.
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.speed = 1;
        }

        // Verificamos si el agente llegó a su destino.
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Detenemos el agente
            agent.isStopped = true;

            // Congelamos la animación de caminar (la dejamos en el frame actual)
            animator.speed = 0;
        }
    }

    public void StartWalking()
    {
        // Reiniciamos el agente y la animación
        agent.isStopped = false;
        animator.speed = 1;

        // Configuramos el destino del agente
        if (agent != null && destination != null)
        {
            agent.SetDestination(destination.position);
        }

        // Activamos el trigger para iniciar la animación de caminar
        if (animator != null)
        {
            animator.SetTrigger("Walk");
        }
    }
}
