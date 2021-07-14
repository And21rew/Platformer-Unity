﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] block;
    public Sprite btnActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MarkBox")
        {
            GetComponent<SpriteRenderer>().sprite = btnActive;
            GetComponent<CircleCollider2D>().enabled = false;
            foreach (GameObject obj in block)
            {
                Destroy(obj);
            }
        }
    }
}

