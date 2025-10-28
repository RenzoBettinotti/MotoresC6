using System.Collections;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    private Animator anim;

    [Header("Config")]
    public float tiempoAnimacion = 2f; // duración de la animación antes de destruirse

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa la animación
            anim.SetBool("Safe", true);

            // Arranca la corrutina que espera y destruye el objeto
            StartCoroutine(EsperarYDestruir());
        }
    }

    IEnumerator EsperarYDestruir()
    {
        // Espera la duración de la animación
        yield return new WaitForSeconds(tiempoAnimacion);


        // Se destruye el civil
        Destroy(gameObject);
    }
}
