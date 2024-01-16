using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;
using YG;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private float initialVolume = 0;
    private Dictionary<string, SoundFile> soundFileByName;
    [SerializeField] private SoundFile[] soundFiles;
    // [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup sfxAudioGroup;
    [SerializeField] private AudioMixerGroup musicAudioGroup;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }

        soundFileByName = new();
        foreach (SoundFile sound in soundFiles)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = sound.audioGroup switch
            {
                AudioGroupType.Music => musicAudioGroup,
                AudioGroupType.SFX => sfxAudioGroup,
                _ => throw new Exception("The new audio group has not been processed"),
            };
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;

            soundFileByName.Add(sound.clip.name, sound);
        }
    }

    // void OnEnable()
    // {
    //     YandexGame.OpenFullAdEvent += SetSilence;
    //     YandexGame.OpenVideoEvent += SetSilence;
    //     YandexGame.CloseFullAdEvent += ResetSilence;
    //     YandexGame.CloseVideoEvent += ResetSilence;
    //     YandexGame.ErrorFullAdEvent += ResetSilence;
    //     YandexGame.ErrorVideoEvent += ResetSilence;
    // }

    // void OnDisable()
    // {
    //     YandexGame.OpenFullAdEvent -= SetSilence;
    //     YandexGame.OpenVideoEvent -= SetSilence;
    //     YandexGame.CloseFullAdEvent -= ResetSilence;
    //     YandexGame.CloseVideoEvent -= ResetSilence;
    //     YandexGame.ErrorFullAdEvent -= ResetSilence;
    //     YandexGame.ErrorVideoEvent -= ResetSilence;
    // }

    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus || (YandexGame.SDKEnabled && YandexGame.nowAdsShow));
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused || (YandexGame.SDKEnabled && YandexGame.nowAdsShow));
    }

    public void Silence(bool silence)
    {
        AudioListener.pause = silence;
        AudioListener.volume = silence ? 0 : 1;
        // mixer.SetFloat("MasterVolume", silence ? 0 : initialVolume);
        // initialVolume = YandexGame.savesData.masterVolume;
    }

    public void SetSilence() => Silence(true);
    public void ResetSilence() => Silence(false);

    public bool IsPlaying(string soundName)
    {
        return soundFileByName[soundName].source.isPlaying;
    }

    public void Play(string soundName)
    {
        soundFileByName[soundName].source.Play();
    }

    public void Stop(string soundName)
    {
        var soundFile = soundFileByName[soundName];
        if (!soundFile.IsUnityNull() && !soundFile.source.IsUnityNull())
        {
            soundFile.source.Stop();
        }
    }
}
