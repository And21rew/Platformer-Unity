using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicvolume"))
            PlayerPrefs.SetInt("musicvolume", 5);

        if (!PlayerPrefs.HasKey("soundvolume"))
            PlayerPrefs.SetInt("soundvolume", 5);
    }

    public void OpenSceneCafe()
    {
        SceneManager.LoadScene(1);
    }
}
