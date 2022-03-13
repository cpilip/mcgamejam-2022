using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public int fadeTime;
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
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
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            if (string.Equals(s.name.Substring(0, 3), "mus"))
            {
                s.source.loop = true;
                s.source.volume = 0;
                s.source.Play();    // audio designer intended for track switching this way
            }
        }
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
        s.source.pitch = Random.Range(.75f, 1.25f);
        s.source.Play();
    }

    IEnumerator FadeOut(string name)
    {
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
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
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found");
            yield break;
        }
        float vol = 0.778f;    // stores original volume
        s.source.volume = 0;            // sets source volume to 0 to start fade
        // s.source.Play();
        while (s.source.volume < vol)
        {
            s.source.volume += (Time.deltaTime / fadeTime);  // slowly increases volume
            yield return null;
        }
        s.source.volume = vol;  // ensures volume will match exactly
    }
}