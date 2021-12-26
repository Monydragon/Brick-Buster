using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public int lives = 3;
    public int score = 0;
    public int numberOfBricks;
    public bool gameOver;
    public int currentLevelIndex = 0;

    public TMP_Text livesText;
    public TMP_Text scoreText;
    public TMP_Text gameoverScoreText;
    public TMP_Text nextLevelScoreText;
    public TMP_Text nextLevelText;

    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;


    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        if (GM != null && GM != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("PLAYER_LEVEL");
        if(currentLevelIndex == 0)
        {
            PlayerPrefs.SetInt("PLAYER_SCORE", 0);
            PlayerPrefs.SetInt("PLAYER_LIVES", 3);
        }
        if (PlayerPrefs.HasKey("PLAYER_SCORE"))
        {
            score = PlayerPrefs.GetInt("PLAYER_SCORE");
        }

        if (PlayerPrefs.HasKey("PLAYER_LIVES"))
        {
            lives = PlayerPrefs.GetInt("PLAYER_LIVES");
        }
        livesText.text = $"Lives: {lives}";
        scoreText.text = $"Score: {score}";
        UpdateBricks();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int val)
    {
        score += val;
        scoreText.text = $"Score: {score}";
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = $"Lives: {lives}";
        if(lives <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            gameoverScoreText.text = $"Final Score: {score}";

        }
    }

    public void GainLife(int val)
    {
        lives += val;
        livesText.text = $"Lives: {lives}";
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("PLAYER_SCORE", 0);
        PlayerPrefs.SetInt("PLAYER_LIVES", 3);
        PlayerPrefs.SetInt("PLAYER_LEVEL", 0);
        SceneManager.LoadScene("Level1");
    }

    public void UpdateBricks()
    {
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        if (numberOfBricks <= 1)
        {
            gameOver = true;
            loadLevelPanel.SetActive(true);
            nextLevelScoreText.text = $"Score: {score}";
            nextLevelText.text = $"Congratulations\nLevel {currentLevelIndex + 1} Completed";
        }
    }
    public void NextLevel()
    {
        currentLevelIndex = PlayerPrefs.GetInt("PLAYER_LEVEL");
        currentLevelIndex++;
        PlayerPrefs.SetInt("PLAYER_SCORE", score);
        PlayerPrefs.SetInt("PLAYER_LIVES", lives);
        PlayerPrefs.SetInt("PLAYER_LEVEL", currentLevelIndex);
        SceneManager.LoadScene(currentLevelIndex);
    }
}
