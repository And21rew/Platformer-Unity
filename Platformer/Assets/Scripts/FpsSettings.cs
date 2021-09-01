using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsSettings : MonoBehaviour
{
    [SerializeField] private Slider fpsSlider;
    [SerializeField] private Text fpsSliderText;

    void Start()
    {
        if (!PlayerPrefs.HasKey("fps"))
            PlayerPrefs.SetInt("fps", 60);

        fpsSlider.value = PlayerPrefs.GetInt("fps");
        Application.targetFrameRate = PlayerPrefs.GetInt("fps");
    }

    void Update()
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("fps");
        PlayerPrefs.SetInt("fps", (int)fpsSlider.value);
        fpsSliderText.text = fpsSlider.value.ToString();
    }
}
