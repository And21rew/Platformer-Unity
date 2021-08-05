using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public UnityEngine.UI.Button[] lvls;
    public UnityEngine.UI.Button heart, bluegm, greengm, doublejump;
    public Text coinText, heartText, bluegemText, greengemText;
    public int maxCountBonus = 5;
    public Slider musicSlider, soundSlider;
    public Text musicSliderText, soundSliderText;
    public UnityEngine.UI.Button dino1, dino2, dino3, dino1Lib, dino2Lib, dino3Lib;

    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))
        {
            for (int i = 0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;
            }
        }

        if (!PlayerPrefs.HasKey("hp"))
            PlayerPrefs.SetInt("hp", 0);

        if (!PlayerPrefs.HasKey("bluegem"))
            PlayerPrefs.SetInt("bluegem", 0);

        if (!PlayerPrefs.HasKey("greengem"))
            PlayerPrefs.SetInt("greengem", 0);

        if (!PlayerPrefs.HasKey("musicvolume"))
            PlayerPrefs.SetInt("musicvolume", 5);

        if (!PlayerPrefs.HasKey("soundvolume"))
            PlayerPrefs.SetInt("soundvolume", 10);

        musicSlider.value = PlayerPrefs.GetInt("musicvolume");
        soundSlider.value = PlayerPrefs.GetInt("soundvolume");

        if (!PlayerPrefs.HasKey("dino1"))
        {
            PlayerPrefs.SetInt("dino1", 0);
            dino1Lib.interactable = false;
        } 
        else if (PlayerPrefs.GetInt("dino1") == 0)
        {
            dino1Lib.interactable = false;
        }
        else
        {
            dino1Lib.interactable = true;
        }

        if (!PlayerPrefs.HasKey("dino2"))
        {
            PlayerPrefs.SetInt("dino2", 0);
            dino2Lib.interactable = false;
        }
        else if (PlayerPrefs.GetInt("dino2") == 0)
        {
            dino2Lib.interactable = false;
        }
        else
        {
            dino2Lib.interactable = true;
        }

        if (!PlayerPrefs.HasKey("dino3"))
        {
            PlayerPrefs.SetInt("dino3", 0);
            dino3Lib.interactable = false;
        }
        else if (PlayerPrefs.GetInt("dino3") == 0)
        {
            dino3Lib.interactable = false;
        }
        else
        {
            dino3Lib.interactable = true;
        }

        if (!PlayerPrefs.HasKey("jump"))
            PlayerPrefs.SetInt("jump", 1);
        else if (PlayerPrefs.GetInt("jump") == 1)
            doublejump.interactable = true;
        else if (PlayerPrefs.GetInt("jump") == 2)
            doublejump.interactable = false;
    }

    void Update()
    {
        PlayerPrefs.SetInt("musicvolume", (int)musicSlider.value);
        musicSliderText.text = musicSlider.value.ToString();

        PlayerPrefs.SetInt("soundvolume", (int)soundSlider.value);
        soundSliderText.text = soundSlider.value.ToString();

        if (PlayerPrefs.HasKey("coins"))
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        else
            coinText.text = "0";

        if (PlayerPrefs.HasKey("hp"))
            heartText.text = PlayerPrefs.GetInt("hp").ToString();
        else
            heartText.text = "0";

        if (PlayerPrefs.HasKey("bluegem"))
            bluegemText.text = PlayerPrefs.GetInt("bluegem").ToString();
        else
            bluegemText.text = "0";

        if (PlayerPrefs.HasKey("greengem"))
            greengemText.text = PlayerPrefs.GetInt("greengem").ToString();
        else
            greengemText.text = "0";

        if(PlayerPrefs.GetInt("dino1") == 1)
        {
            dino1Lib.interactable = true;
            dino1.interactable = false;
        }

        if (PlayerPrefs.GetInt("dino2") == 1)
        {
            dino2Lib.interactable = true;
            dino2.interactable = false;
        }

        if (PlayerPrefs.GetInt("dino3") == 1)
        {
            dino3Lib.interactable = true;
            dino3.interactable = false;
        }
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Buy_hp(int cost)
    {
        if ((PlayerPrefs.GetInt("coins") >= cost) && (PlayerPrefs.GetInt("hp") < maxCountBonus))
        {
            PlayerPrefs.SetInt("hp", PlayerPrefs.GetInt("hp") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_BlueGem(int cost)
    {
        if ((PlayerPrefs.GetInt("coins") >= cost) && (PlayerPrefs.GetInt("bluegem") < maxCountBonus))
        {
            PlayerPrefs.SetInt("bluegem", PlayerPrefs.GetInt("bluegem") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_GreenGem(int cost)
    {
        if ((PlayerPrefs.GetInt("coins") >= cost) && (PlayerPrefs.GetInt("greengem") < maxCountBonus))
        {
            PlayerPrefs.SetInt("greengem", PlayerPrefs.GetInt("greengem") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

    public void Buy_DoubleJump(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("jump", PlayerPrefs.GetInt("jump") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
            doublejump.interactable = false;
        }
    }

    public void Buy_dino1(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("dino1", PlayerPrefs.GetInt("dino1") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
            dino1.interactable = false;
        }
    }

    public void Buy_dino2(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("dino2", PlayerPrefs.GetInt("dino2") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
            dino2.interactable = false;
        }
    }

    public void Buy_dino3(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("dino3", PlayerPrefs.GetInt("dino3") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
            dino3.interactable = false;
        }
    }
}
