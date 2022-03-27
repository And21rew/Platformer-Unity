using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCafe : MonoBehaviour
{
    [SerializeField] private GameObject cafeScreen;

    void Start()
    {
        if (!PlayerPrefs.HasKey("cafe"))
        {
            PlayerPrefs.SetInt("cafe", 0);
        }

        if (PlayerPrefs.GetInt("cafe") == 1)
        {
            cafeScreen.SetActive(true);
            StartCoroutine(HoldCafe());
        }
    }

    IEnumerator HoldCafe()
    {
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("cafe", 0);
    }
}
