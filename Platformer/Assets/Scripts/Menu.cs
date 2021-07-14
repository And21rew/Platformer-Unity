using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public UnityEngine.UI.Button[] lvls;
    public UnityEngine.UI.Button heart, bluegm, greengm;
    public Text coinText, heartText, bluegemText, greengemText;
    public int maxCountBonus = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))
            for (int i = 0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;
            }

        if (!PlayerPrefs.HasKey("hp"))
            PlayerPrefs.SetInt("hp", 0);

        if (!PlayerPrefs.HasKey("bluegem"))
            PlayerPrefs.SetInt("bluegem", 0);

        if (!PlayerPrefs.HasKey("greengem"))
            PlayerPrefs.SetInt("greengem", 0);
    }

    // Update is called once per frame
    void Update()
    {
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
}
