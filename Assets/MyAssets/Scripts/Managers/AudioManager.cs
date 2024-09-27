using System;
using UnityEngine;

// Enums
public enum Musics
{
    introduction,
    round_1,
    round_2,
    round_3,
    round_4,
    round_5,
    round_6,
    the_end
};

public enum Sfxs
{
    jump,
    explosion,
    shoot,
    scream_1,
    scream_2,
    scream_3,
    scream_4,
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

    public void PlayMusicRound(int round)
    {
        switch (round)
        {
            case 1:
                PlayMusic(Musics.round_1);
                break;
            case 2:
                PlayMusic(Musics.round_2);
                break;
            case 3:
                PlayMusic(Musics.round_3);
                break;
            case 4:
                PlayMusic(Musics.round_4);
                break;
            case 5:
                PlayMusic(Musics.round_5);
                break;
            case 6:
                PlayMusic(Musics.round_6);
                break;
            case 7:
                PlayMusic(Musics.the_end);
                break;
            default:
                PlayMusic(Musics.introduction);
                break;
        }
    }

    internal void PlayMusic(Musics musicEnum)
    {
        string musicName = musicEnum.ToString();
        AudioClip music = Array.Find(musicSounds, x => x.name == musicName);
        if (music == null)
        {
            Debug.LogWarning("Music Not Found");
        }
        else
        {
            Debug.Log("Playing " + music);
            musicSource.loop = true;
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
