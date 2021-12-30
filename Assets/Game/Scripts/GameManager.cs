using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using yaSingleton;

[CreateAssetMenu(fileName = "Game Manager", menuName = "Singletons/GameManager")]
public class GameManager : Singleton<GameManager>
{
    public int lives = 3;
    public int score = 0;
    public int numberOfBricks;
    public int currentLevelIndex = 1;

    private void OnEnable()
    {
        EventManager.onGameReset += EventManager_onGameReset;
        EventManager.onScoreChanged += EventManager_onScoreChanged;
        EventManager.onLivesChanged += EventManager_onLivesChanged;
        EventManager.onWinLevel += EventManager_onWinLevel;
        EventManager.onLoseLevel += EventManager_onLoseLevel;
        EventManager.onBrickHit += EventManager_onBrickHit;
        EventManager.onBrickDestroyed += EventManager_onBrickDestroyed;
    }

    private void EventManager_onGameReset()
    {
        score = 0;
        lives = 3;
        currentLevelIndex = 1;
        UpdateBricks();
    }

    private void EventManager_onBrickDestroyed()
    {
        UpdateBricks();
    }

    private void EventManager_onBrickHit()
    {
    }

    private void EventManager_onLoseLevel()
    {
        //ResetGame();
    }

    private void EventManager_onWinLevel()
    {
        //NextLevel();
    }

    private void EventManager_onLivesChanged(int value)
    {
        lives += value;
        if(lives <= 0)
        {
            EventManager.LevelFail();
        }
    }

    private void EventManager_onScoreChanged(int value)
    {
        score += value;
    }

    private void OnDisable()
    {
        EventManager.onGameReset -= EventManager_onGameReset;
        EventManager.onScoreChanged -= EventManager_onScoreChanged;
        EventManager.onLivesChanged -= EventManager_onLivesChanged;
        EventManager.onWinLevel -= EventManager_onWinLevel;
        EventManager.onLoseLevel -= EventManager_onLoseLevel;
        EventManager.onBrickHit -= EventManager_onBrickHit;
        EventManager.onBrickDestroyed -= EventManager_onBrickDestroyed;
    }

    public void ResetGame()
    {
        EventManager.GameReset();
        SceneManager.LoadScene("Level1");
    }

    public void UpdateBricks()
    {
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        if (numberOfBricks <= 1)
        {
            EventManager.LevelComplete();
        }
    }
    public void NextLevel()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentLevelIndex);
    }
}
