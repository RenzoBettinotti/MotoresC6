using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Foam : MonoBehaviour
{
    [Header("FoamCaracteristics")]
    [SerializeField] protected float _foamSpeed;
    [SerializeField] protected float _foamLifeTime;
    
    Rigidbody _rb;


   
    void Awake()
    {
        _rb.GetComponent<Rigidbody>();
        Destroy(gameObject,_foamLifeTime);
    }

    void Launch(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        _rb.linearVelocity = direction * _foamSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fire")) 
        {
            Destroy(gameObject);
        }
    }
}
