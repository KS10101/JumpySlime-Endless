using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager instance;

    [SerializeField] private Button playButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button leadboardButton;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject inGameUICanvas;
    [SerializeField] private GameObject gameOverMenuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject highscoreCanvas;
    [SerializeField] private GameObject leaderboardCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject storeCanvas;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip highscoreSFX;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        AudioManager.instance.PlayBGSound(bgmClip);
        inGameUICanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);
        RestartButton.onClick.AddListener(Restart);
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        leadboardButton.onClick.AddListener(BuildLeaderboard);
        settingsButton.onClick.AddListener(OpenSettings);
        storeButton.onClick.AddListener(OpenStore);
        MainMenuButton.onClick.AddListener(GotoMainMenu);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayGame);
        RestartButton.onClick.RemoveListener(Restart);
        submitButton.onClick.RemoveListener(OnSubmitButtonClick);
        leadboardButton.onClick.RemoveListener(BuildLeaderboard);
        settingsButton.onClick.RemoveListener(OpenSettings);
        storeButton.onClick.RemoveListener(OpenStore);
        MainMenuButton.onClick.RemoveListener(GotoMainMenu);
        ExitButton.onClick.RemoveListener(ExitGame);
    }

    private string SetName()
    {
        string inputText = nameInput.text;
        return inputText;
    }

    private void OnSubmitButtonClick()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        if (SetName() != string.Empty)
        {
            string nameText = SetName();
            int score = ScoreManager.instance.GetCurrentScore();

            Debug.Log($"Name - {SetName()} Score - {score} Current Score - {ScoreManager.instance.GetCurrentScore()}");
            if (Leaderboard.instance.GetEntryCount() < 8)
                Leaderboard.instance.AddScoreCard(nameText, score);
            else if (Leaderboard.instance.GetEntryCount() >= 8)
                Leaderboard.instance.AddScoreAtEnd(nameText, score);
            highscoreCanvas.SetActive(false);
            
            gameOverMenuCanvas.SetActive(true);
            currentScore.text = ScoreManager.instance.GetCurrentScore().ToString();

        }

    }

    public void RunGameOverCoroutine()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        AudioManager.instance.StopBGSound();
        AudioManager.instance.PlaySFX(gameOverSFX);
        PlayerController.instance.StopPlayer();
        gameOverCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameOverCanvas.SetActive(false);
        OnGameOver();
    }

    private void OnGameOver()
    {
        inGameUICanvas.SetActive(false);

        if ((Leaderboard.instance.GetEntryCount() < 8 && ScoreManager.instance.GetCurrentScore() != 0) ||
            (Leaderboard.instance.GetEntryCount() == 8 && ScoreManager.instance.GetCurrentScore() > Leaderboard.instance.GetLowestScore()))
        {
            highScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            highscoreCanvas.SetActive(true);
            AudioManager.instance.PlaySFX(highscoreSFX);

        }
        else
        {
            currentScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            highscoreCanvas.SetActive(false);
            gameOverMenuCanvas.SetActive(true);
            
        }

    }


    private void PlayGame()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        PlayerController.instance.StartPlayer();
        inGameUICanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void Restart()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        ScoreManager.instance.ResetLocalScore();
        LivesManager.instance.ResetLives();
        PlayerController.instance.StopPlayer();
        LevelGenerator.instance.Restart();
        PlayerController.instance.StartPlayer();
        gameOverMenuCanvas.SetActive(false);
        inGameUICanvas.SetActive(true);
    }

    private void ResetLevel()
    {
        ScoreManager.instance.ResetLocalScore();
        LivesManager.instance.ResetLives();
        PlayerController.instance.StopPlayer();
        LevelGenerator.instance.Restart();
        inGameUICanvas.SetActive(false);
        PlayerController.instance.SetAniamtionSpeed(1);
    }

    private void BuildLeaderboard()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        gameOverMenuCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        leaderboardCanvas.SetActive(true);
        //Leaderboard.instance.ClearScoresFromJson();
        //Leaderboard.instance.SaveScoresToJSON();
        Leaderboard.instance.CreateEntry();
    }

    private void OpenSettings()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        settingsCanvas.SetActive(true);
    }

    private void OpenStore()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        storeCanvas.SetActive(true);
    }

    private void GotoMainMenu()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        gameOverMenuCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        ResetLevel();
    }

    private void ExitGame()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        Application.Quit();
    }
}