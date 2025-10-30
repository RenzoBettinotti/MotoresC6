using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shoot Setting")]
    [SerializeField] Foam foam;
    [SerializeField] Transform foamSpawner;  

    ObjectPooler objectPooler;

    void Awake()
    {
        objectPooler = ObjectPooler.Instance;
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
       
            GameObject f = objectPooler.SpawnFromPool("Foam", foamSpawner.position, foamSpawner.rotation);

            foam = f.GetComponent<Foam>();

            if (foam != null)
            {
                foam.Launch(foamSpawner.forward);
            }


        
    }
}
