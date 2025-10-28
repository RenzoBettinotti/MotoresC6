using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Axe : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle")) 
            
        {
            Destroy(gameObject);
        }
    }
}
