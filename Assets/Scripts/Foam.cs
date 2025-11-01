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
        _rb = GetComponent<Rigidbody>();

        
    }

    public void Launch(Vector3 direction)
    {

        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.linearVelocity = direction.normalized * _foamSpeed;
        transform.forward = direction.normalized;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fire")) 
        {
            Destroy(collision.gameObject);
        }

        
        ObjectPooler.Instance.ReturnToPool(this.gameObject);
    }
}
