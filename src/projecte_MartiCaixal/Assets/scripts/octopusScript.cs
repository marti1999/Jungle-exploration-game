using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octopusScript : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    public GameObject game;
    public int health = 1;
    public GameObject deathAnimation;
    private AudioSource audioPlayer;
    public AudioClip shootClip;



    float fireRate;
    float nextFire;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
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
        audioPlayer = GetComponent<AudioSource>();
        fireRate = 3f;
        nextFire = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (game.GetComponent<gameController>().state == gameController.GameState.playing)
        {
            CheckIfTimeToFire();
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            audioPlayer.clip = shootClip;
            audioPlayer.Play();
        }

    }
}
