using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LeaderboardButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private AudioClip ClickSFX;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private GameObject LeaderboardPanel;

    private void Start()
    {
        AudioManager.instance.PlayBGSound(bgmClip);
    }

    private void OnEnable()
    {
        LeaderboardButton.onClick.AddListener(BuildLeaderboard);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        LeaderboardButton.onClick.RemoveListener(BuildLeaderboard);
        ExitButton.onClick.RemoveListener(ExitGame);
    }

    

    private void BuildLeaderboard()
    {
        AudioManager.instance.PlaySFX(ClickSFX);
        LeaderboardPanel.SetActive(true);
        //Leaderboard.instance.ClearScoresFromJson();
        Leaderboard.instance.CreateEntry();
    }

    private void ExitGame()
    {
        AudioManager.instance.PlaySFX(ClickSFX);
        Application.Quit();
    }
}
