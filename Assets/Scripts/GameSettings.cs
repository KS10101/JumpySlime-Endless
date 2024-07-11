using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Button BGM_ToggleBut;
    [SerializeField] private Button SFX_ToggleBut;
    [SerializeField] private Button CloseBut;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject BGMToggleON;
    [SerializeField] private GameObject SFXToggleON;

    [SerializeField] private AudioClip sfx_Clip;
    [SerializeField] private AudioClip clickSFX;

    private void OnEnable()
    {
        CloseBut.onClick.AddListener(CloseSettings);
        BGM_ToggleBut.onClick.AddListener(ToggleBGM);
        SFX_ToggleBut.onClick.AddListener(ToggleSFX);
    }

    private void OnDisable()
    {
        CloseBut.onClick.RemoveListener(CloseSettings);
        BGM_ToggleBut.onClick.RemoveListener(ToggleBGM);
        SFX_ToggleBut.onClick.RemoveListener(ToggleSFX);
    }

    private void CloseSettings()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        settingsPanel.SetActive(false);
    }

    private void ToggleBGM()
    {
        BGMToggleON.SetActive(!BGMToggleON.activeSelf);
        if (BGMToggleON.activeSelf)
            AudioManager.instance.ToggleBGAudio(0.25f);
        else
            AudioManager.instance.ToggleBGAudio(0f);
    }

    private void ToggleSFX()
    {
        SFXToggleON.SetActive(!SFXToggleON.activeSelf);
        if (SFXToggleON.activeSelf)
        {
            AudioManager.instance.ToggleSFXAudio(1f);
            AudioManager.instance.PlaySFX(sfx_Clip);
        }
        else
            AudioManager.instance.ToggleSFXAudio(0f);
    }

}
