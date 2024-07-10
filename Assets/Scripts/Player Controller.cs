using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using FlightKit;

[RequireComponent(typeof(LaneRunner))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    LaneRunner runner;
    float speed = 10f;
    private bool _enableControls = false;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] string animationSpeedMultiplierName;
    [SerializeField] GameObject CountdownCanvas;

    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip countdownClip;

    public bool EnableControls { get => _enableControls; set => _enableControls = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        runner = GetComponent<LaneRunner>();
        speed = runner.followSpeed;
        runner.followSpeed = 0f;
        SetAniamtionSpeed(1);
    }

    private void Start()
    {
        //StartCoroutine(Countdown());
        Debug.Log($"speed - {runner.followSpeed}");

    }

    public void StartPlayer()
    {

        SetSpeed(10);
        EnableControls = true;

    }

    private void Update()
    {
        if (_enableControls)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) runner.lane--;
            if (Input.GetKeyDown(KeyCode.RightArrow)) runner.lane++;
        }

        Debug.Log($"animation Speed: {PlayerAnimator.GetFloat("SpeedRate")}");
    }

    //private IEnumerator Countdown()
    //{
    //    CountdownCanvas.SetActive(true);
    //    CountdownCanvas.GetComponent<Animator>().enabled = true;
    //    AudioManager.instance.PlaySFX(countdownClip);
    //    yield return new WaitForSeconds(4f);
    //    if (LevelGenerator.instance.ready)
    //        runner.followSpeed = speed;
    //    Debug.Log("Player Start Moving");
    //    SetAniamtionSpeed(1);
    //    CountdownCanvas.GetComponent<Animator>().enabled = false;
    //    CountdownCanvas.SetActive(false);
    //    AudioManager.instance.PlayBGSound(backgroundClip);
    //}

    public void SetSpeed(float speed)
    {
        if (speed <= 30 && speed >= 6f)
        {
            this.speed = speed;
            this.runner.followSpeed = this.speed;
        }
        else if (speed < 6 && speed > 0)
        {
            this.speed = 6f;
            this.runner.followSpeed = this.speed;
        }
        else if (this.speed <= 0)
        {
            this.speed = 0;
            this.runner.followSpeed = 0;
        }
        Debug.Log($"speed : {runner.followSpeed}");

        SetAniamtionSpeed(this.runner.followSpeed / 10);
    }

    public void StopPlayer()
    {
        EnableControls = false;
        this.speed = 0;
        this.runner.followSpeed = 0;
    }

    public void SetAniamtionSpeed(float rate)
    {
        if (rate <= 2 && rate >= 0)
            PlayerAnimator?.SetFloat(animationSpeedMultiplierName, rate);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
