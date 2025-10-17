using UnityEngine;

public class ObjectoInteractivo : MonoBehaviour
{
    [SerializeField] int civilesSalvados;

    public void SalvarCivil() 
    {
        Destroy(gameObject);
        civilesSalvados++;
    }
}
