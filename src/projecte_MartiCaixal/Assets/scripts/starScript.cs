using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starScript : MonoBehaviour
{
        float lastHitTime;

    private AudioSource audioPlayer;

    void Start()
    {

        audioPlayer = GetComponent<AudioSource>();
        lastHitTime = Time.time;



    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > lastHitTime + 1)
        {
            GetComponent<CircleCollider2D>().enabled = true;

        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {

        GetComponent<CircleCollider2D>().enabled = false;

        if (other.gameObject.tag == "player")
        {

            if (Time.time > lastHitTime + 1)
            {

                audioPlayer.Play();
                //      gameObject.vis(false);
                GetComponent<Renderer>().enabled = false;
                lastHitTime = Time.time;
                //Debug.Log("played coin");


                Destroy(gameObject, 1f);

            }

        
        }



     

    }
}
