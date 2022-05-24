using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCharacterMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public PlayerStats healthController;
    public Animator animator;

    public AudioClip coinClip;
    //   public AudioClip hurtBulletclip;
    public AudioClip dieClip;
    public AudioClip jumpClip;
    public AudioClip hurtMeleeClip;
    public AudioClip winClip;
    private AudioSource audioPlayer;
    private bool showed = false;
    private ParticleSystem bloodParticle;

    private Rigidbody2D rigidBody;
    bool isColliding;


    //public Text score;

    float lastHitTime;
    float lastRataTime;
    float lastCrabTime;
    float lastCoinTime;
    float lastBallTime;
    private float lastHurtAnimationTime;
    private float animationHurtDuration = 2f;
    public GameObject uiFinalScore;


    public float horizontalAxis = 0f;
    private bool jumpFlag = false;

    public float horizontalSpeed = 1f;
    private bool jump = false;
    private bool crouch = false;
    public GameObject game;
    // public GameObject weapon;
    public GameObject meleeHurtSound;

    private SpriteRenderer renderer;
    private bool animationHurt;
    float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        bloodParticle = GetComponentInChildren<ParticleSystem>();
        lastHitTime = Time.time;
        lastRataTime = Time.time;
        lastCrabTime = Time.time;
        lastCoinTime = Time.time;
        lastBallTime = Time.time;
        lastHurtAnimationTime = Time.deltaTime;
        renderer = GetComponent<SpriteRenderer>();

        
    }

    public void increaseElapsedTime()
    {
        timer += Time.deltaTime;
        int seconds = Mathf.RoundToInt(timer % 60);
      
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(game.GetComponent<gameController>().state);



        if (game.GetComponent<gameController>().state == gameController.GameState.playing)
        {
            //es poden canviar els inputs a edit, project settings, input
            increaseElapsedTime();


            float currentHealth = healthController.getHealth();

            if (currentHealth <= 0f)
            {
                playerDies();
            }

            isColliding = false;


            horizontalAxis = Input.GetAxisRaw("Horizontal") * horizontalSpeed;


            animator.SetFloat("Speed", Mathf.Abs(horizontalAxis));

            if (jumpFlag)
            {
                animator.SetBool("isJumping", true);
                jumpFlag = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (!animator.GetBool("isJumping"))
                {
                    audioPlayer.clip = jumpClip;
                    audioPlayer.Play();
                }
                jump = true;

                //   animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            //if (Time.deltaTime - lastHurtAnimationTime > 1)
            //{
               
            //}

            //if (animationHurt)
            //{
            //    if (Time.deltaTime - lastHurtAnimationTime < animationHurtDuration) 
            //    {
            //        renderer.color = Color.red;
            //        lastHurtAnimationTime = Time.deltaTime;
            //    }
            //    else
            //    {
            //        Debug.Log("white");
            //        renderer.color = Color.white;
            //        animationHurt = false;
            //    }

            //}


        }



    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);


        renderer.color = Color.white;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    private void playerDies()
    {
        game.GetComponent<gameController>().state = gameController.GameState.dead;
        animator.SetBool("isDead", true);
        //    horizontalSpeed = 0f;

        //rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.bodyType = RigidbodyType2D.Dynamic;


        audioPlayer.clip = dieClip;
        audioPlayer.Play();
        bloodParticle.Play();
        // animator.Play("Player_Die");
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "instantDeath")
        {
            renderer.color = Color.red;
            StartCoroutine(ExecuteAfterTime(1));
            Debug.Log("muere (de aburrimiento)");
            healthController.TakeDamage(20f);

            playerDies();

        }

        if (other.gameObject.tag == "coin")
        {

            if (Time.time > lastCoinTime + 2)
            {
                lastCoinTime = Time.time;
                
                game.SendMessage("increasePoints");

                // score = game.GetComponent<gameController>().uiScore.
            }

        }

        if (other.gameObject.tag == "potion")
        {
            //healthController.TakeDamage(1f);
            healthController.Heal(1f);
        }

        if (other.gameObject.tag == "weapon")
        {
            Debug.Log("getWeapon");

            //healthController.TakeDamage(1f);
            GetComponent<playerWeapon>().isActive = true;
        }

        if (other.gameObject.tag == "gem")
        {

            animator.Play("Player_Idle");
           
            game.GetComponent<gameController>().state = gameController.GameState.completed;


            if (!showed)
            {
                uiFinalScore.SetActive(true);

                int max = getMaxScore();

                if (max > Mathf.RoundToInt(timer % 60))
                {
                    saveScore(Mathf.RoundToInt(timer % 60));
                    uiFinalScore.GetComponentInChildren<Text>().text =
                        "New record!!\nBest time: " + Mathf.RoundToInt(timer % 60).ToString();
                }
                else
                {
                    uiFinalScore.GetComponent<Text>().text = "Time: "+ Mathf.RoundToInt(timer % 60).ToString() + "\nBest time: " + max.ToString();
                }
                showed = true;
            }
            

            Time.timeScale = 0;
        }

        if (other.gameObject.tag == "ball")
        {

            if (Time.time > lastBallTime + 3)
            {
                renderer.color = Color.red;
                StartCoroutine(ExecuteAfterTime(1));

                lastBallTime = Time.time;
                other.GetComponent<PolygonCollider2D>().enabled = false;
                healthController.TakeDamage(2f);
                Instantiate(meleeHurtSound, transform.position, Quaternion.identity);
            }



        }

        if (other.gameObject.tag == "rata")
        {

            if (Time.time > lastRataTime + 1)
            {
                renderer.color = Color.red;
                StartCoroutine(ExecuteAfterTime(1));

                healthController.TakeDamage(1f);

                //controller.Move(horizontalAxis, true, true);
                //jumpFlag = true;
                //jump = false;
                Instantiate(meleeHurtSound, transform.position, Quaternion.identity);

                //  audioPlayer.clip = hurtMeleeClip;
                //   audioPlayer.Play();
                //  Debug.Log("rata hit");
                Debug.Log(lastRataTime.ToString());
                lastRataTime = Time.time;

            }


            //isColliding = true;



            // score = game.GetComponent<gameController>().uiScore.


        }

        if (other.gameObject.tag == "crab")
        {

            if (Time.time > lastCrabTime + 1)
            {
                //animationHurt = true;
                //lastHurtAnimationTime = Time.deltaTime;
                renderer.color = Color.red;
                StartCoroutine(ExecuteAfterTime(1));
               // lastHurtAnimationTime = Time.deltaTime;

                healthController.TakeDamage(2f);

                Instantiate(meleeHurtSound, transform.position, Quaternion.identity);

                //      audioPlayer.clip = hurtMeleeClip;
                //   audioPlayer.Play();
                Debug.Log(lastRataTime.ToString());
                lastCrabTime = Time.time;

            }


            //isColliding = true;



            // score = game.GetComponent<gameController>().uiScore.


        }



        if (other.gameObject.tag == "bulletOctopus")
        {

            if (lastHitTime != Time.time)
            {
                renderer.color = Color.red;
                StartCoroutine(ExecuteAfterTime(1));

                lastHitTime = Time.time;

                Debug.Log("tocat");
                //   audioPlayer.clip = hurtBulletclip;
                // audioPlayer.Play();
                healthController.TakeDamage(1);
            }

        }


    }

    public int getMaxScore()
    {
        return PlayerPrefs.GetInt("record", 999999);
    }

    public void saveScore(int time)
    {
        PlayerPrefs.SetInt("record", time);
    }



    void FixedUpdate()
    {
        if (game.GetComponent<gameController>().state == gameController.GameState.playing)
        {
            // el fixed delta time 'es el valor que ha passat desde que la mateixa funcio ha sigut cridada. aixi ens movem la mateixa quantitat sense tenir en compte el numero de vegades que s'ha cridat (vamos, que fa que la velocitat sigui consistent en tots els sistemes i plataformes)
            controller.Move(horizontalAxis, crouch, jump);
            if (jump)
            {
                jumpFlag = true;
                jump = false;
            }
        }
        //controller.Move(horizontalAxis * Time.fixedDeltaTime, false, false);
    }
}
