using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int CurrentScore = 0;
    private int MaxScore = 0;
    public int scoreMultiplier = 1;
    public TextMeshProUGUI ScoreTextField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        UpdateScoreText(0);
    }

    public void AddScore(int score)
    {
        Debug.Log("Score Multiplier : " + scoreMultiplier);
        CurrentScore = CurrentScore + (score * scoreMultiplier);
        SetMaxScore(CurrentScore);
        UpdateScoreText(CurrentScore);
    }

    public void ReduceScore(int score)
    {
        if (CurrentScore > 0 && CurrentScore >= score)
            CurrentScore = CurrentScore - score;
        else
            CurrentScore = 0;
        SetMaxScore(CurrentScore);
        UpdateScoreText(CurrentScore);
    }

    public int GetCurrentScore()
    {
        return CurrentScore;
    }

    public void ResetLocalScore()
    {
        CurrentScore = 0;
        MaxScore = 0;
        UpdateScoreText(CurrentScore);
    }

    public int GetMaxScore()
    {
        return MaxScore;
    }

    public void SetMaxScore(int score)
    {
        if(score > MaxScore)
            MaxScore = score;
    }

    public void UpdateScoreText(int score)
    {
        ScoreTextField.text = score.ToString();
    }

    public void SaveCurrentCoinData()
    {
        int currentMaxCoins = PlayerPrefs.GetInt("Coins") + CurrentScore;
        SaveCoinsData(currentMaxCoins);
    }

    public void AddnSaveCoinsData(int coin)
    {
        int newCoins = ScoreManager.instance.GetCoinsData() + coin;
        ScoreManager.instance.SaveCoinsData(newCoins);
    }

    public void SaveCoinsData(int coins)
    {
        PlayerPrefs.SetInt("Coins", coins);
        Debug.Log("Coins Saved in playerpref");
    }

    public int GetCoinsData()
    {
        return PlayerPrefs.GetInt("Coins");
    }

}
