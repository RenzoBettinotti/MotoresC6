using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shoot Setting")]
    [SerializeField] Foam foam;
    [SerializeField] Transform foamSpawner;  

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
            
        }

        
    }
    void Shoot()
    {
        

        GameObject f = objectPooler.SpawnFromPool("Foam", foamSpawner.transform.position, foamSpawner.transform.rotation);
        

        foam = f.GetComponent<Foam>();

            if (foam != null)
            {
                foam.Launch(foamSpawner.forward);
            }


        
    }
}
