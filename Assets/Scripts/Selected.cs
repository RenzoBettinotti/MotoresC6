using System;
using UnityEngine;

public class Selected : MonoBehaviour
{

    [Header("Selection")]
    [SerializeField] LayerMask mask;
    [SerializeField] public float distancia = 1.5f;
    void Start()
    {
        mask = LayerMask.GetMask("RayCast Detect");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask)) 
        {
            if (hit.collider.tag == "Civil") 
            {
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    hit.collider.transform.GetComponent<ObjectoInteractivo>().SalvarCivil();
                    Console.WriteLine("Interactuo");
                }
            }
        }
    }
}
