using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    float moveSpeed = 4f;

    Rigidbody2D rb;
    public GameObject hitSound;

    GameObject target;
   // public GameObject player;
    Vector2 moveDirection;

    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindGameObjectWithTag("player");
            //target.transform.position.y += 1f;
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y +0.3f);
            Destroy(gameObject, 5f);

        } catch(Exception ex)
        {

        }
        
    }

    void OnTriggerEnter2D(Collider2D col)   
    {
        if (col.gameObject.tag.Equals("bee") || col.gameObject.tag.Contains("arrier"))
        {

        } else
        {
            if (col.gameObject.tag.Equals("player"))
            {
                //   Debug.Log("tocat");
                Instantiate(hitSound, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
            else
            {
                //Debug.Log("fallo");

                Destroy(gameObject);
            }
        }

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
