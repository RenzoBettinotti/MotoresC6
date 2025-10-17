using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SmokeMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _speed = 5.0f; 
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.transform.position = new Vector3(0,-20,0);
    }

    
    void Update()
    {
        rb.transform.position += Vector3.up*_speed*Time.deltaTime;
    }
}
