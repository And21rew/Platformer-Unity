using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("fps"))
            PlayerPrefs.SetInt("fps", 60);

        Application.targetFrameRate = PlayerPrefs.GetInt("fps");

        if (!PlayerPrefs.HasKey("musicvolume"))
            PlayerPrefs.SetInt("musicvolume", 5);

        if (!PlayerPrefs.HasKey("soundvolume"))
            PlayerPrefs.SetInt("soundvolume", 5);

        if (!PlayerPrefs.HasKey("final"))
            PlayerPrefs.SetInt("final", 0);

        if (!PlayerPrefs.HasKey("gameover"))
            PlayerPrefs.SetInt("gameover", 0);
    }

    public void OpenSceneCafe()
    {
        SceneManager.LoadScene(1);
    }
}
