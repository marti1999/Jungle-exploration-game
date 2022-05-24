using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabScript : MonoBehaviour
{
    public int health = 5;
    public GameObject deathAnimation;
    private bool dirRight = false;
    public float speed = 2.0f;
    public GameObject game;
    public Animator animator;
    float lastHitTime;


    public void TakeDamage(int damage)
    {
        Debug.Log(health.ToString());
        health -= damage;
        if (health <= 0)
        {
            Die();
        }



    }

    private void Die()
    {
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Start()
    {
        lastHitTime = Time.time;

    }

    void Update()
    {
        if (game.GetComponent<gameController>().state == gameController.GameState.playing)
        {
            animator.Play("Crab_Walk");

            if (dirRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
        else if (game.GetComponent<gameController>().state == gameController.GameState.dead)
        {
            //    Destroy(gameObject);
            animator.Play("Crab_Idle");

            //animator.StopPlayback();
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
