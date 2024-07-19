using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    [SerializeField] private Button BGM_ToggleBut;
    [SerializeField] private Button SFX_ToggleBut;
    [SerializeField] private Button CloseBut;
    [SerializeField] private Button helpBut;
    [SerializeField] private Button helpCloseBut;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject BGMToggleON;
    [SerializeField] private GameObject SFXToggleON;

    [SerializeField] private AudioClip sfx_Clip;
    [SerializeField] private AudioClip clickSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        UpdateSettings();
    }

    private void OnEnable()
    {
        CloseBut.onClick.AddListener(CloseSettings);
        BGM_ToggleBut.onClick.AddListener(ToggleBGM);
        SFX_ToggleBut.onClick.AddListener(ToggleSFX);
        helpBut.onClick.AddListener(OpenHelpPanel);
        helpCloseBut.onClick.AddListener(CloseHelpPanel);
    }

    private void OnDisable()
    {
        CloseBut.onClick.RemoveListener(CloseSettings);
        BGM_ToggleBut.onClick.RemoveListener(ToggleBGM);
        SFX_ToggleBut.onClick.RemoveListener(ToggleSFX);
        helpBut.onClick.RemoveListener(OpenHelpPanel);
        helpCloseBut.onClick.RemoveListener(CloseHelpPanel);
    }

    private void CloseSettings()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        settingsPanel.SetActive(false);
    }

    private void ToggleBGM()
    {
        BGMToggleON.SetActive(!BGMToggleON.activeSelf);
        ToggleBGMVolume();
        SaveSettings("BGM", BGMToggleON.activeSelf);
    }

    private void ToggleBGMVolume()
    {
        if (BGMToggleON.activeSelf)
            AudioManager.instance.ToggleBGAudio(0.25f);
        else
            AudioManager.instance.ToggleBGAudio(0f);
    }

    private void ToggleSFX()
    {
        SFXToggleON.SetActive(!SFXToggleON.activeSelf);
        if (SFXToggleON.activeSelf)
            AudioManager.instance.PlaySFX(sfx_Clip);

        ToggleSFXVolume();
        SaveSettings("SFX", SFXToggleON.activeSelf);
    }

    private void ToggleSFXVolume()
    {
        if (SFXToggleON.activeSelf)
            AudioManager.instance.ToggleSFXAudio(1f);
        else
            AudioManager.instance.ToggleSFXAudio(0f);
    }

    public void UpdateSettings()
    {
        BGMToggleON.SetActive(LoadSettings("BGM"));
        ToggleBGMVolume();

        SFXToggleON.SetActive(LoadSettings("SFX"));
        ToggleSFXVolume();
    }

    public void SaveSettings(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    private bool LoadSettings(string key)
    {
        bool value = Convert.ToBoolean(PlayerPrefs.GetInt(key));
        return value;
    }

    private void OpenHelpPanel()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        helpPanel.SetActive(true);
    }

    private void CloseHelpPanel()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        helpPanel.SetActive(false);
    }
}
