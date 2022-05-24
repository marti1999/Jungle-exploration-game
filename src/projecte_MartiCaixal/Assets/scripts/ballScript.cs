using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject explosionSoundPrefab;

    private int timesHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void explosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Contains("barrier"))
        {
            //InvokeRepeating("explosion", 0f, generatorTimer);
            //TODO: fer l' animacio de destroy i el so
            // Instantiate(hitSound, transform.position, Quaternion.identity);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, .1f);


        }

        if (col.gameObject.tag.Contains("ball"))
        {
            if (timesHit > 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Instantiate(explosionSoundPrefab, transform.position, Quaternion.identity);

                Destroy(gameObject, .1f);
            }
            else
            {
                timesHit++;
            }
        }
    }



}
