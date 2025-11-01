using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{

    [Header("Salud")]
    [SerializeField] float health;
    [SerializeField] float oxygen;


    public float Health { get { return health; } set { health = value; } }
    public float Oxygen { get { return oxygen; } set { oxygen = value; } }
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update() 
    {
        if (health == 0)
        {
            Death();
        }
        else if (oxygen == 0) 
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Smoke")) 
        {
            ReduceOxygen(5f);
        }
        
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire")) 
        {
            TakeDamage(1f);
            rb.transform.position = new Vector3(0,0,0);
        }
    }


    public void TakeDamage(float damage) 
    {
        AudioManager.instance.DamageSFX();
        health -= damage *Time.deltaTime;
    }
    public void ReduceOxygen(float reduction)
    {
        oxygen -= reduction * Time.deltaTime;
    }

    public void Death() 
    {
        AudioManager.instance.DeathSFX();
        Destroy(gameObject,5f);
    }
}
