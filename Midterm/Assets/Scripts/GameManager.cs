using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    [SerializeField] private float timeLimit = 60f;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject timeoutScreen;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private Image correctCollectablePanel;
    [SerializeField] private Image incorrectCollectablePanel;

    [Header("Materials")]
    [SerializeField] private Material correctCollectableMaterial;
    [SerializeField] private Material incorrectCollectableMaterial;

    [Header("Local Variables")]
    private int score = 0;
    private float currentTime;
    private int highScore;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        LoadHighScore();
        SetRandomCollectableColors();
    }

    public void Start()
    {
        currentTime = timeLimit;
        StartCoroutine(CountdownTimer());
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            ReloadGame();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + newScore;
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    private void SetRandomCollectableColors()
    {
        Color correctColor = new Color(Random.value, Random.value, Random.value);
        Color incorrectColor = new Color(Random.value, Random.value, Random.value);

        correctCollectableMaterial.color = correctColor;
        incorrectCollectableMaterial.color = incorrectColor;

        correctCollectablePanel.color = correctColor;
        incorrectCollectablePanel.color = incorrectColor;
    }

    private IEnumerator CountdownTimer()
    {
        while (currentTime > 0)
        {
            timeText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
            
            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        timeText.text = "Time: 0";
        OnTimeLimitReached();
    }

    private void OnTimeLimitReached()
    {
        finalScoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        highScoreText.text = "High Score: " + highScore;
        
        timeoutScreen.SetActive(true);
        SoundManager.instance.PlayTimeOverSfx();

        Time.timeScale = 0;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
