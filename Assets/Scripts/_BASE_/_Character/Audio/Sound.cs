using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    public bool loop;

    [Range(0.0001f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public AudioMixerGroup group;

    [HideInInspector]
    public AudioSource source;
}
