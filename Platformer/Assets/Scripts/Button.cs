using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] block;
    [SerializeField] private Sprite btnActive;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MarkBox"))
        {
            GetComponent<SpriteRenderer>().sprite = btnActive;
            GetComponent<CircleCollider2D>().enabled = false;
            foreach (GameObject obj in block)
                Destroy(obj);
        }
    }
}

