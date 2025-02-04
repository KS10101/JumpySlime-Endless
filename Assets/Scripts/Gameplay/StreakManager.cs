using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StreakManager : MonoBehaviour
{
    public static StreakManager instance;
 
    private int streak = 0;
    [SerializeField] private int scoreStreak = 0;
    [SerializeField] private int desiredStreak = 3;
    [SerializeField] private int multiplier = 1;
    [SerializeField] private Animator streakAnimator;
    [SerializeField] private TextMeshProUGUI streakNum;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        streakAnimator.enabled = false;
    }

    public void MakeHealthStreak()
    {
        streak++;
        if (streak >= desiredStreak)
        {
            streak = 0;
            PlayerController.instance.StopPlayer();
            LevelGameManager.instance.RunGameOverCoroutine();
        }
    }

    public void MakeScoreStreak()
    {
        streakAnimator.StopPlayback();
        scoreStreak++;
        if (scoreStreak >= desiredStreak)
        {
            multiplier ++;
            ScoreManager.instance.scoreMultiplier = multiplier;
            streakAnimator.gameObject.SetActive(true);
            streakNum.text = multiplier.ToString() + "X";
            streakAnimator.enabled = true;
            streakAnimator.SetTrigger("streak");
        }
    }

    public void ResetStreak()
    {
        if(streak >= 0)
        {
            streak = 0;
        }
    }

    public void ResetScoreStreak()
    {
        if (scoreStreak >= 0)
        {
            scoreStreak = 0;
            multiplier = 1;
            ScoreManager.instance.scoreMultiplier = multiplier;
            streakAnimator.enabled = false;
            streakAnimator.gameObject.SetActive(false);
        }
    }

}
