using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private Dictionary<string, SoundFile> soundFileByName;
    [SerializeField] private SoundFile[] soundFiles;
    [SerializeField] private AudioMixerGroup mixer;

    public void Awake()
    {
        Instance = this;

        soundFileByName = new();
        foreach (Sound sound in soundFiles)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = mixer;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
            soundFileByName.Add(sound.name, sound);
        }
    }

    public void Play(string soundName)
    {
        soundFileByName[soundName].source.Play();
    }

    public void Stop(string soundName)
    {
        soundFileByName[soundName].source.Stop();
    }
}