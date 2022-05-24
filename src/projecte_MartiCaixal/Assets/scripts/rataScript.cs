using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rataScript : MonoBehaviour
{


    private bool dirRight = false;
    public float speed = 2.0f;
    float lastHitTime;
    public GameObject game;
    public Animator animator;


    // Start is called before the first frame   update
    void Start()
    {
        lastHitTime = Time.time;
    }





    void Update()
    {
        if (game.GetComponent<gameController>().state == gameController.GameState.playing)
        {
            if (dirRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
        else if (game.GetComponent<gameController>().state == gameController.GameState.dead)
        {
        //    Destroy(gameObject);

            animator.StopPlayback();
        }
        //if (transform.position.x >= 4.0f)
        //{
        //    dirRight = false;
        //}

        //if (transform.position.x <= -4)
        //{
        //    dirRight = true;
        //}


    }

    void OnTriggerEnter2D(Collider2D other)
    {




        if (other.gameObject.tag == "rataBarrier")
        {
            if (lastHitTime != Time.time)
            {
                lastHitTime = Time.time;
                dirRight = !dirRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;

            }

        }
    }
}