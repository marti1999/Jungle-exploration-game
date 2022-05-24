using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactAnimation;
    //public AudioSource audio;
    public GameObject shootingSound;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(shootingSound, transform.position, transform.rotation);

        rb.velocity = transform.right * speed;
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        crabScript crab = other.GetComponent<crabScript>();
        if (crab != null)
        {
            crab.TakeDamage(2);  
        }

        octopusScript bee = other.GetComponent<octopusScript>();
       if (bee != null) 
        {
            bee.TakeDamage(3);
        }



        Instantiate(impactAnimation, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
