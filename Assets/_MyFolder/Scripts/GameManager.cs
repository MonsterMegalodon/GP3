using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public ScoreKeeper scoreKeeper;
    private int score;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        NewGame();
        highScoreText.text = ("HighScore ") + scoreKeeper.GetHighScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }    
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        if (score > scoreKeeper.GetHighScore())
        {
            scoreKeeper.SetScore(score);
        }
    }

    private void NewGame()
    {

        //if (score > scoreKeeper.GetHighScore())
        //{
        //    scoreKeeper.SetScore(score);
        //}

        score = 0;
        scoreText.text = score.ToString();


    }

    public void IncreaseScore(int amount)
    {
        score += amount ;
        scoreText.text = score.ToString();
    }

}
