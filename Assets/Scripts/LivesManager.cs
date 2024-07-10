using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    private int maxLives = 3;
    private int currentLives;
    [SerializeField] private Image[] livesImg;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI(currentLives);
    }

    public void AddLives()
    {
        if (currentLives < maxLives)
            currentLives++;
        else
            currentLives = maxLives;

        UpdateLivesUI(currentLives);
    }

    public void ReduceLives()
    {
        if (currentLives > 0)
            currentLives--;
        else
            currentLives = 0;

        UpdateLivesUI(currentLives);
        if (currentLives <= 0)
            LevelGameManager.instance.RunGameOverCoroutine();
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesUI(currentLives);
    }

    public void UpdateLivesUI(int Currentlives)
    {
        foreach (Image img in livesImg)
        {
            img.enabled = false;
        }

        for (int i = 0; i < Currentlives; i++)
        {
            livesImg[i].enabled = true;
        }
    }
}
