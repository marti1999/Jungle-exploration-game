using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlattformScript : MonoBehaviour
{
    public float amplitude;
    public float speed;
    private Vector3 tempPos;
    private float tempVal;
    public bool alReves;


    // Start is called before the first frame update
    void Start()
    {
        tempVal = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        //tempPos.y = transform.position.y;
        ////tempPos.x = transform.position.x;
        //tempPos.x = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        //transform.position = tempPos;

        if (alReves)
        {
            tempPos.y = tempVal + (-amplitude) * Mathf.Sin(speed * Time.time);
        }
        else
        {
            tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        }
       
        tempPos.x = transform.position.x;
        transform.position = tempPos;
    }
}
