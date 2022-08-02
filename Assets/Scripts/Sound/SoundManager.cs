using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public int fadeTime;
    public Sound[] sounds;
    public Sound[] music;

    public Slider masterVolumeSlider, SFXVolumeSlider, musicVolumeSlider;
    public float masterVolume, SFXVolume, musicVolume;

    public static SoundManager instance;

    void Awake()
    {
        // ensures only a single instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // initialize sound and music arrays
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
        // two arrays makes volume settings more managable
        foreach (Sound m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.loop = true;   // music clips will loop
            m.source.volume = 0;    // music plays always, fading in and out as necessary
            m.source.Play();
        }

        // initialize volume values to default
        masterVolumeSlider.value = 1f;
        musicVolumeSlider.value = .75f;
        SFXVolumeSlider.value = 1f;
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        // master volume set by slider
        masterVolume = masterVolumeSlider.value;
        // sfx and music volume set by slider and master modifier
        SFXVolume = SFXVolumeSlider.value * masterVolume;
        musicVolume = musicVolumeSlider.value * masterVolume;
        foreach (Sound s in sounds)
        {
            s.volume = SFXVolume;
        }
        // volume only editable from menu, only need to change menu music volume
        // variable will handle volume when future sounds fade in
        Sound menu = System.Array.Find(music, Sound => Sound.name == "musicMenu");
        menu.source.volume = musicVolume;
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            return;
        }
        s.source.Stop();
    }

    public void StartFadeIn(string name)
    {
        StartCoroutine(FadeIn(name));
    }

    public void StartFadeOut(string name)
    {
        StartCoroutine(FadeOut(name));
    }

    public void BeepSpeak(string name)
    {
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            return;
        }
        s.source.pitch = Random.Range(.9f, 1.1f);
        s.source.Play();
    }

    IEnumerator FadeOut(string name)
    {
        // fadeIn/fadeOut currently only work for music bc of the array setup
        Sound s = System.Array.Find(music, Sound => Sound.name == name);    
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            yield break;
        }
        while (s.source.volume > 0)
        {
            s.source.volume -= (Time.deltaTime / fadeTime);  // slowly decreases volume
            yield return null;
        }
        // Stop(name);             // once silent, stop
        // s.source.volume = vol;  // once stopped, reset to original volume
    }

    IEnumerator FadeIn(string name)
    {
        // fadeIn/fadeOut currently only work for music bc of the array setup
        Sound s = System.Array.Find(music, Sound => Sound.name == name);    
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            yield break;
        }
        s.source.volume = 0;            // sets source volume to 0 to start fade

        // increases up to musicVolume, managed by settings
        while (s.source.volume < musicVolume)
        {
            s.source.volume += (Time.deltaTime / fadeTime);  // slowly increases volume
            yield return null;
        }
        s.source.volume = musicVolume;  // ensures volume will match exactly
    }
}