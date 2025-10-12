using UnityEngine;

using Unity.Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineCamera targetCamera;
    [SerializeField] private int priorityBoost = 20;

    private int originalPriority;

    private void Awake()
    {
        if (targetCamera != null)
            originalPriority = targetCamera.Priority;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetCamera != null)
        {
            Debug.Log("Jugador entró al trigger -> Activando cámara: " + targetCamera.name);
            targetCamera.Priority = originalPriority + priorityBoost;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && targetCamera != null)
        {
            Debug.Log("Jugador salió del trigger -> Volviendo prioridad");
            targetCamera.Priority = originalPriority;
        }
    }
}

