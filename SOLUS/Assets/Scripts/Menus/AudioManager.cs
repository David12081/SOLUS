using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource music;
    public AudioClip[] clips;
    int currentScene;

    private bool isPlaying;

    public Sound[] sounds;

    private void Start()
    {
        music.clip = clips[0];
        music.Play();
        isPlaying = true;

        music.volume = 0.2f;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != currentScene)
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            if ((currentScene == 0) && isPlaying == false)
            {
                music.clip = clips[0];
                music.Play();
                isPlaying = true;

            }
            else if ((currentScene == 0) && isPlaying == true)
            {
                music.clip = clips[0];
            }
            else if(currentScene == 1 || currentScene == 4)
            {
                isPlaying = false;
                music.Stop();
                music.clip = clips[1];
                music.Play();
            }
            else if (currentScene == 2)
            {
                isPlaying = false;
                music.Stop();
                music.clip = clips[3];
                music.Play();
            }
            else if (currentScene == 3 || currentScene == 5)
            {
                isPlaying = false;
                music.Stop();
                music.clip = clips[2];
                music.Play();
            }
        }
    }
}