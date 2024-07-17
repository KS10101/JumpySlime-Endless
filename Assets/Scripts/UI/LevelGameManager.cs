using Dreamteck.Forever;
using System.Collections;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager instance;

    [SerializeField] private Button playButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button leadboardButton;
    [SerializeField] private Button GOLeadboardButton;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button characterButton;
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
    [SerializeField] private GameObject characterSelectCanvas;
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
        GOLeadboardButton.onClick.AddListener(BuildLeaderboard);
        settingsButton.onClick.AddListener(OpenSettings);
        characterButton.onClick.AddListener(OpenCharacterSelect);
        storeButton.onClick.AddListener(OpenShop);
        MainMenuButton.onClick.AddListener(GotoMainMenu);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayGame);
        RestartButton.onClick.RemoveListener(Restart);
        submitButton.onClick.RemoveListener(OnSubmitButtonClick);
        leadboardButton.onClick.RemoveListener(BuildLeaderboard);
        GOLeadboardButton.onClick.RemoveListener(BuildLeaderboard);
        settingsButton.onClick.RemoveListener(OpenSettings);
        characterButton.onClick.RemoveListener(OpenCharacterSelect);
        storeButton.onClick.RemoveListener(OpenShop);
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

        PlayerController.instance.TriggerAnimation("Rig_Damage_02");
        AudioManager.instance.StopBGSound();
        AudioManager.instance.PlaySFX(gameOverSFX);
        PlayerController.instance.StopPlayer();
        gameOverCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameOverCanvas.SetActive(false);
        LevelGenerator.instance.Restart();
        OnGameOver();
    }

    private void OnGameOver()
    {
        ScoreManager.instance.SaveCurrentCoinData();
        inGameUICanvas.SetActive(false);
        nameInput.text = string.Empty;
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
        AudioManager.instance.PlayBGSound(bgmClip);

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
        StreakManager.instance.ResetScoreStreak();
        PlayerController.instance.StopPlayer();
        //LevelGenerator.instance.Restart();
        PlayerController.instance.StartPlayer();
        PlayerController.instance.SetAnimationSpeed(1);
        gameOverMenuCanvas.SetActive(false);
        inGameUICanvas.SetActive(true);
    }

    private void ResetLevel()
    {
        ScoreManager.instance.ResetLocalScore();
        LivesManager.instance.ResetLives();
        StreakManager.instance.ResetScoreStreak();
        PlayerController.instance.StopPlayer();
        //LevelGenerator.instance.Restart();
        inGameUICanvas.SetActive(false);
        PlayerController.instance.TriggerAnimation("Jump");
        PlayerController.instance.SetAnimationSpeed(1);
    }

    private void BuildLeaderboard()
    {
        AudioManager.instance.PlaySFX(clickSFX);
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

    private void OpenCharacterSelect()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        CharacterManager.instance.UpdateCharStateList();
        CharacterManager.instance.InitiateStoreItems();
        CharacterSelectPanel.instance.UpdateCoinText(ScoreManager.instance.GetCoinsData());
        characterSelectCanvas.SetActive(true);
    }

    private void OpenShop()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        StoreItemManager.instance.GenerateStoreItems();
        StorePanel.instance.UpdateCoinText(ScoreManager.instance.GetCoinsData());
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