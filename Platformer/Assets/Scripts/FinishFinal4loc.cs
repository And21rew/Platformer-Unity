using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFinal4loc : MonoBehaviour
{
    [SerializeField] private Main main;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("final", 0);
            PlayerPrefs.SetInt("gameover", 1);
            main.Win();
        }
    }
}
