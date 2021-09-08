using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFinal : MonoBehaviour
{
    [SerializeField] private GameObject[] block;
    [SerializeField] private Sprite SwitchOn;
    public AudioSource audioSource;
    public AudioClip stoneSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(stoneSound);
            GetComponent<Animator>().SetBool("Off", false);
            GetComponent<BoxCollider2D>().enabled = false;
            foreach (GameObject obj in block)
                obj.SetActive(false);
        }
    }
}
