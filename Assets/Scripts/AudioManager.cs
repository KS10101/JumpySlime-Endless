using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Instance
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    [SerializeField] private AudioSource BG_Audio;
    [SerializeField] private AudioSource SFX_Audio;
    [SerializeField] private AudioClip default_BGM_clip;
    [SerializeField] private AudioClip default_SFX_clip;


    private void Start()
    {
        //PlayBGSound();
        SFX_Audio.loop = false;
    }

    public void ToggleBGAudio(float vol)
    {
        BG_Audio.volume = vol;
    }

    public void ToggleSFXAudio(float vol)
    {
        SFX_Audio.volume = vol;
    }

    public void PlayBGSound(AudioClip clip = null)
    {
        if (clip == null)
            BG_Audio.clip = default_BGM_clip;
        else
            BG_Audio.clip = clip;

        BG_Audio.clip = clip;
        BG_Audio.loop = true;
        if (BG_Audio.clip != null)
            BG_Audio.Play();
    }

    public void PlaySFX(AudioClip clip = null)
    {
        if (clip == null)
            SFX_Audio.clip = default_SFX_clip;
        else
            SFX_Audio.clip = clip;

        SFX_Audio.loop = false;
        SFX_Audio.Play();
    }

    public void StopBGSound()
    {
        BG_Audio.Stop();
    }
}

