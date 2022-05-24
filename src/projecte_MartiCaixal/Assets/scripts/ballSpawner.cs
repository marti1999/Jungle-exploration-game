using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{




    public GameObject enemyPreFab;
    public GameObject explosionPrefab;

    public float generatorTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        startGenerator();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void createInstance()
    {

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Instantiate(enemyPreFab, transform.position, Quaternion.identity);
    }

    public void startGenerator()
    {
        InvokeRepeating("createInstance", 0f, generatorTimer);
    }

 

}