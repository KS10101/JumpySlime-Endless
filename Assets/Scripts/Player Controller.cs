using Dreamteck.Forever;
using System;
using UnityEngine;


[RequireComponent(typeof(LaneRunner))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public delegate void GameEvents();
    public static event GameEvents OnSwitchCharacter;

    LaneRunner runner;
    float speed = 10f;
    private bool _enableControls = false;

    GameObject GameChar;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] string animationSpeedMultiplierName;

    public bool EnableControls { get => _enableControls; set => _enableControls = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        runner = GetComponent<LaneRunner>();
        speed = runner.followSpeed;
        runner.followSpeed = 0f;
        
    }

    private void OnEnable()
    {
        OnSwitchCharacter += InitPlayer;
    }

    private void OnDisable()
    {
        OnSwitchCharacter -= InitPlayer;
    }

    private void Start()
    {
        SwitchCharacter();
        //StartCoroutine(Countdown());
        SetAnimationSpeed(1);
        Debug.Log($"speed - {runner.followSpeed}");
    }

    public void SwitchCharacter() => OnSwitchCharacter?.Invoke();

    private void InitPlayer()
    {
        //GameObject characterPrefab = CharacterManager.instance.GetSelectedCharacter();
        //GameChar = Instantiate(characterPrefab, this.gameObject.transform);
        CharacterManager.instance.ActivateCharacter(this.transform);
        GameChar = this.gameObject.transform.GetChild(0).gameObject;
        GameChar.transform.localPosition = new Vector3(0, -1.1f, 0);
        PlayerAnimator = GameChar.GetComponent<Animator>();
        TriggerAnimation("Jump");
    }

    public void StartPlayer()
    {
        TriggerAnimation("Jump");
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

        SetAnimationSpeed(this.runner.followSpeed / 10);
    }

    public void StopPlayer()
    {
        EnableControls = false;
        this.speed = 0;
        this.runner.followSpeed = 0;
    }

    public void TriggerAnimation(string animName)
    {
        PlayerAnimator.Play(animName);
        SetAnimationSpeed(1);
    }

    public void SetAnimationSpeed(float rate)
    {
        if (rate <= 2 && rate >= 0)
            PlayerAnimator?.SetFloat(animationSpeedMultiplierName, rate);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
