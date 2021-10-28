using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }
}
