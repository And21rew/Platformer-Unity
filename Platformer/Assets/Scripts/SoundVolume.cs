using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume : MonoBehaviour
{
    public AudioSource musicSource, soundSource;

    private void Start()
    {
        musicSource.volume = (float)PlayerPrefs.GetInt("musicvolume") / 10;
        soundSource.volume = (float)PlayerPrefs.GetInt("soundvolume") / 10;
    }

    private void Update()
    {
        musicSource.volume = (float)PlayerPrefs.GetInt("musicvolume") / 10;
        soundSource.volume = (float)PlayerPrefs.GetInt("soundvolume") / 10;
    }
}
