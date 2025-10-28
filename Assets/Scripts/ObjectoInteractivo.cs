using UnityEngine;
using System;

public class ObjectoInteractivo : MonoBehaviour
{
    [SerializeField] int civilesSalvados;
    public static event Action OnCivilianRescued;
    public void SalvarCivil() 
    {
        OnCivilianRescued?.Invoke();
        Destroy(gameObject,5f);
        civilesSalvados++;
    }
}
