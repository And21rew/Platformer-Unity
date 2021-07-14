using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int hp = 0, bluegem = 0, greengem = 0;
    public Sprite[] numbers;
    public Sprite is_hp, no_hp;
    public Sprite is_bluegem, no_bluegem;
    public Sprite is_greengem, no_greengem;
    public Sprite is_key, no_key;
    public Image hp_img, bluegem_img, greengem_img, key_img;
    public Player player;
    public UnityEngine.UI.Button heart, blue, green;
    public int maxCountBonus = 5;

    private void Start()
    {
        if (PlayerPrefs.GetInt("hp") > 0)
        {
            hp = PlayerPrefs.GetInt("hp");
            hp_img.sprite = is_hp;
            hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
        }

        if (PlayerPrefs.GetInt("bluegem") > 0)
        {
            bluegem = PlayerPrefs.GetInt("bluegem");
            bluegem_img.sprite = is_bluegem;
            bluegem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bluegem];
        }

        if (PlayerPrefs.GetInt("greengem") > 0)
        {
            greengem = PlayerPrefs.GetInt("greengem");
            greengem_img.sprite = is_greengem;
            greengem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[greengem];
        }
    }

    public void Add_hp()
    {
        if (hp < maxCountBonus)
        {
            hp++;
            hp_img.sprite = is_hp;
            hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
        }
    }

    public void Add_bluegem()
    {
        if (bluegem < maxCountBonus)
        {
            bluegem++;
            bluegem_img.sprite = is_bluegem;
            bluegem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bluegem];
        }
    }

    public void Add_greengem()
    {
        if (greengem < maxCountBonus)
        {
            greengem++;
            greengem_img.sprite = is_greengem;
            greengem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[greengem];
        }
    }

    public void Add_key()
    {
        key_img.sprite = is_key;
    }

    public void Use_hp()
    {
        if (hp > 0)
        {
            hp--;
            player.RecountHp(1);
            hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
            if (hp == 0)
                hp_img.sprite = no_hp;
            StartCoroutine(WaitForUseHeart());
        }
    }

    public void Use_bluegem()
    {
        if (bluegem > 0)
        {
            bluegem--;
            player.BlueGems();
            bluegem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bluegem];
            if (bluegem == 0)
                bluegem_img.sprite = no_bluegem;
            StartCoroutine(WaitForUseBlueGem());
        }
    }

    public void Use_greengem()
    {
        if (greengem > 0)
        {
            greengem--;
            player.GreenGems();
            greengem_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[greengem];
            if (greengem == 0)
                greengem_img.sprite = no_greengem;
            StartCoroutine(WaitForUseGreenGem());
        }
    }

    IEnumerator WaitForUseHeart()
    {
        heart.interactable = false;
        yield return new WaitForSeconds(5f);
        heart.interactable = true;
    }

    IEnumerator WaitForUseBlueGem()
    {
        blue.interactable = false;
        yield return new WaitForSeconds(5f);
        blue.interactable = true;
    }

    IEnumerator WaitForUseGreenGem()
    {
        green.interactable = false;
        yield return new WaitForSeconds(5f);
        green.interactable = true;
    }

    public void RecountItems()
    {
        PlayerPrefs.SetInt("hp", hp);
        PlayerPrefs.SetInt("bluegem", bluegem);
        PlayerPrefs.SetInt("greengem", greengem);
    }
}
