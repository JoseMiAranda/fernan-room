using System;
using UnityEngine;

// Enums
public enum Musics
{
    coocking
};

public enum Sfxs
{
    jump,
    explosion,
    shoot
};

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] musicSounds, sfxSounds;
    private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        if (Instance == null) // Singleton pattern
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There are one more Audio Managers!");
        }
    }

    public void PlayMusic(Musics musicEnum)
    {
        string musicName = musicEnum.ToString();
        AudioClip music = Array.Find(musicSounds, x => x.name == musicName);
        if(music == null)
        {
            Debug.LogWarning("Music Not Found");
        } else
        {
            Debug.Log("Playing " + music);
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    public void PlaySfx(Sfxs sfxEnum)
    {
        string sfxName = sfxEnum.ToString();
        AudioClip sfx = Array.Find(sfxSounds, x => x.name == sfxName);
        if (sfx == null)
        {
            Debug.LogWarning("SFX Not Found: " + sfxName);
        }
        else
        {
            Debug.Log("Playing " + sfx);
            sfxSource.PlayOneShot(sfx);
        }
    }
}
