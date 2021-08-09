using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public AudioSource musicSource, soundSource;
    public Slider soundSlider, musicSlider;
    public Text soundSliderText, musicSliderText;

    private void Awake()
    {
        var soundVolume = PlayerPrefs.GetInt("soundvolume");
        var musicVolume = PlayerPrefs.GetInt("musicvolume");
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        soundSliderText.text = soundVolume.ToString();
        musicSliderText.text = musicVolume.ToString();
        soundSource.volume = (float)soundVolume / 10;
        musicSource.volume = (float)musicVolume / 10;
    }

    public void ChangeSoundValue()
    {
        PlayerPrefs.SetInt("soundvolume", (int)soundSlider.value);
        soundSliderText.text = soundSlider.value.ToString();
        soundSource.volume = (float)PlayerPrefs.GetInt("soundvolume") / 10;
    }
    
    public void ChangeMusicValue()
    {
        PlayerPrefs.SetInt("musicvolume", (int)musicSlider.value);
        musicSliderText.text = musicSlider.value.ToString();
        musicSource.volume = musicSlider.value / 10;
    }
}
