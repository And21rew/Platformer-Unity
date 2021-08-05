using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject[] block;
    [SerializeField] private Sprite SwitchOn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Off", false);
            GetComponent<BoxCollider2D>().enabled = false;
            foreach (GameObject obj in block)
            {
                obj.SetActive(true);
            }
        }
    }
}
