using UnityEngine;
using System;


public class CivilianRescuer : MonoBehaviour
{
    
    public static event Action OnCivilianRescued;

    private void OnDestroy()
    {
       
        if (gameObject.CompareTag("Civil"))
        {
            
            OnCivilianRescued?.Invoke();
        }
    }
}