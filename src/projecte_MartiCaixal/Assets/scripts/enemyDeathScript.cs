using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathSound;

    void Start()
    {
        Instantiate(deathSound, transform.position, Quaternion.identity);

        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
       // Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
