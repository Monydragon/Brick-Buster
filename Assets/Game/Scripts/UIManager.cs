using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using yaSingleton;

public class UIManager : MonoBehaviour
{

    public TMP_Text livesText;
    public TMP_Text scoreText;

    public TMP_Text gameoverScoreText;
    public TMP_Text nextLevelScoreText;
    public TMP_Text nextLevelText;

    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;

    private void OnEnable()
    {
        EventManager.onGameReset += EventManager_onGameReset;
        EventManager.onLivesChanged += EventManager_onLivesChanged;
        EventManager.onScoreChanged += EventManager_onScoreChanged;
        EventManager.onWinLevel += EventManager_onWinLevel;
        EventManager.onLoseLevel += EventManager_onLoseLevel;
    }

    private void EventManager_onGameReset()
    {
        scoreText.text = $"Score: {GameManager.Instance.score}";
        livesText.text = $"Lives: {GameManager.Instance.lives}";
    }

    private void EventManager_onLoseLevel()
    {
        gameOverPanel.SetActive(true);
        gameoverScoreText.text = $"Final Score: {GameManager.Instance.score}";
    }

    private void EventManager_onWinLevel()
    {
        loadLevelPanel.SetActive(true);
        nextLevelScoreText.text = $"Score: {GameManager.Instance.score}";
        nextLevelText.text = $"Congratulations\nLevel {GameManager.Instance.currentLevelIndex + 1} Completed";
    }

    private void EventManager_onScoreChanged(int value)
    {
        scoreText.text = $"Score: {GameManager.Instance.score}";
    }

    private void EventManager_onLivesChanged(int value)
    {
        livesText.text = $"Lives: {GameManager.Instance.lives}";
    }

    private void OnDisable()
    {
        EventManager.onGameReset -= EventManager_onGameReset;
        EventManager.onLivesChanged -= EventManager_onLivesChanged;
        EventManager.onScoreChanged -= EventManager_onScoreChanged;
        EventManager.onWinLevel -= EventManager_onWinLevel;
        EventManager.onLoseLevel -= EventManager_onLoseLevel;
    }

    public void StartGameNormal()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StartGameEndless()
    {
        SceneManager.LoadScene("LevelCustomGenerated");
    }
}
