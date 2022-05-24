using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemScript : MonoBehaviour
{
    // Start is called before the first frame update
    float lastHitTime;

    private CircleCollider2D collider;
    private AudioSource audioPlayer;

    void Start()
    {

        audioPlayer = GetComponent<AudioSource>();
        lastHitTime = Time.time;

        collider = GetComponent<CircleCollider2D>();


}

// Update is called once per frame
void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "player")
        {
            collider.enabled = false;

            if (Time.time > lastHitTime + 15)
            {
                audioPlayer.Play();
                //      gameObject.vis(false);
                GetComponent<Renderer>().enabled = false;
                lastHitTime = Time.time;
              


                Destroy(gameObject, 7f);

            }


        }





    }
}
