using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    public enum GameState { playing, idle, pause, dead, completed}

    public GameState state = GameState.idle;
    public GameObject uiIdle;
    public GameObject uiPaused;
    public GameObject uiCompleted;
    public GameObject uiFailed;
    public GameObject uiScore;
    private bool showed = false;
    public Text score;
    public Text elapsedTime;
    float timer = 0.0f;

    private int points = 0;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
      //  PlayerPrefs.DeleteAll();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.idle && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            state = GameState.playing;
            uiIdle.SetActive(false);
        }
        if (state == GameState.playing)
        {
            increaseElapsedTime();
        }

        if (state == GameState.playing && (Input.GetKeyDown("escape")))
        {
            Time.timeScale = 0;


            state = GameState.pause;
                    Debug.Log(state);

            uiPaused.SetActive(true);
        }

        if (state == GameState.pause && (Input.GetKeyDown("space")))
        {
            Time.timeScale = 1;

            state = GameState.playing;
            uiPaused.SetActive(false);

        }

        if (state == GameState.completed)
        {
            /*

            if (!showed)
            {
                Time.timeScale = 0;

                showed = true;



                uiCompleted.SetActive(true);

                int max = getMaxScore();

                if (max > Mathf.RoundToInt(timer % 60))
                {
                    saveScore(Mathf.RoundToInt(timer % 60));
                    uiCompleted.GetComponentInChildren<Text>().text =
                        "New record!!\nBest time: " + Mathf.RoundToInt(timer % 60).ToString();
                }
                else
                {
                    uiCompleted.GetComponent<Text>().text = "Best time: " + max.ToString();
                }
            }

            
    */

            uiCompleted.SetActive(true);


            if (Input.GetKeyDown("space"))
            {
                restartGame();

            }
        }

        if (state == GameState.dead)
        {

            uiFailed.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                restartGame();

            }
        }

    }
    public void restartGame()
    {
        resetTimeScale();
        SceneManager.LoadScene("SampleScene");

    }

    public void resetTimeScale(float newTimeScale = 1f)
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = newTimeScale;
       
    }

    public void increasePoints()
    {
        points++;
        score.text = "Score: " + points.ToString();
    }

    public void increaseElapsedTime()
    {
        timer += Time.deltaTime;
        int seconds = Mathf.RoundToInt(timer % 60);
        elapsedTime.text = "Time: " + seconds.ToString();
    }

    public int getMaxScore()
    {
        return PlayerPrefs.GetInt("record",999999);
    }

    public void saveScore(int time)
    {
        PlayerPrefs.SetInt("record", time);
    }
}
