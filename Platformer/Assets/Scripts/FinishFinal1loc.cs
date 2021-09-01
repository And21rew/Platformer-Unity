using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFinal1loc : MonoBehaviour
{
    [SerializeField] private Main main;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("final", 1);
            main.Win();
        }
    }
}
