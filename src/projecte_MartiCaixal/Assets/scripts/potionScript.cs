using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour
{
    float lastHitTime;
    public float amplitude;          
    public float speed;              
    private float tempVal;
    private Vector3 tempPos;
    public float destroyDelay;
    private AudioSource audioPlayer;

    void Start()
    {

        audioPlayer = GetComponent<AudioSource>();
        lastHitTime = Time.time;
        tempVal = transform.position.y;



    }

    void Update()
    {
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.x = transform.position.x;
        transform.position = tempPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("player"))
        {
            GetComponent<CircleCollider2D>().enabled = false;



            audioPlayer.Play();
            //      gameObject.vis(false);
            GetComponent<Renderer>().enabled = false;
            lastHitTime = Time.time;
            Debug.Log("collectible collected");

            //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

              Destroy(gameObject, destroyDelay);
        }






    }





}
