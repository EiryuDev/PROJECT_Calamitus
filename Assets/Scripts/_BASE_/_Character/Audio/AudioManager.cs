using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Globalization;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mymixer;
    public Sound[] sounds;

    //public static AudioManager instance;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = s.group;

            s.source.loop = s.loop;
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    private void Start()
    {
        play("BGM");
    }

    private void Update()
    {
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();
        foreach (Slider s in sliders)
        {
            if (s.gameObject.name == "MasterSlider")
            {
                masterSlider = s;
            }

            else if (s.gameObject.name == "BGMSlider")
            {
                bgmSlider = s;
            }

            else if (s.gameObject.name == "SFXSlider")
            {
                sfxSlider = s;
            }
        }

        masterSlider.value = PlayerPrefs.GetFloat("Master");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");

    }

    public void masterVolume(Slider volume)
    {
        mymixer.SetFloat("MasterVolume", volume.value);
        PlayerPrefs.SetFloat("Master", volume.value);
    }

    public void bgmVolume(Slider volume)
    {
        mymixer.SetFloat("BGMVolume", volume.value);
        PlayerPrefs.SetFloat("BGM", volume.value);
    }

    public void sfxVolume(Slider volume)
    {
        mymixer.SetFloat("SFXVolume", volume.value);
        PlayerPrefs.SetFloat("SFX", volume.value);

    }
}
