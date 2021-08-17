using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float speed = 4f;
    private readonly float TimeToDisable = 1f;

    void Start()
    {
        StartCoroutine(SetDisable());
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(TimeToDisable);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            StopCoroutine(SetDisable());
            gameObject.SetActive(false);
    }
}
