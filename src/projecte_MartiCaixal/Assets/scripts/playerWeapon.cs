using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeapon : MonoBehaviour
{
    public Animator animator;
    public GameObject game;
    [SerializeField]
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform firePoint;
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {

        if (game.GetComponent<gameController>().state == gameController.GameState.playing && isActive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.Play("Player_Cast");
                Shoot();
            }
        }

        
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
