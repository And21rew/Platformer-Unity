using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume : MonoBehaviour
{
    public AudioSource musicSource, soundSource;

    public void ChangeSoundValue()
    {
        soundSource.volume = (float)PlayerPrefs.GetInt("soundvolume") / 10;
    }
    
    public void ChangeMusicValue()
    {
        musicSource.volume = (float)PlayerPrefs.GetInt("musicvolume") / 10;
    }
}
