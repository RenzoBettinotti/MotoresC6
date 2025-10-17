using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Foam : MonoBehaviour
{
    [Header("FoamCaracteristics")]
    [SerializeField] protected float _foamSpeed;
    [SerializeField] protected float _foamLifeTime;
    
    Rigidbody _rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb.GetComponent<Rigidbody>();
        Destroy(gameObject,_foamLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
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
