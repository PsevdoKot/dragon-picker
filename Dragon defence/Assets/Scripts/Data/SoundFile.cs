using UnityEngine;

[System.Serializable]
public class SoundFile
{
    public AudioGroupType audioGroup;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    [HideInInspector] public AudioSource source;
    public bool loop;
    public bool playOnAwake;
}