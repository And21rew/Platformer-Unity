using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject[] ActiveBlocks;
    [SerializeField] private GameObject[] DestroyBlocks;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject obj in ActiveBlocks)
                obj.SetActive(true);

            foreach (GameObject obj in DestroyBlocks)
                Destroy(obj);
        }
    }
}
